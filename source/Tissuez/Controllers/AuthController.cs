using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Owin.Security.Providers.GitHub;
using Tissuez.Models;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace Tissuez.Controllers
{
    /// <summary>
    /// Provides GitHub OAuth authentication.
    /// </summary>
    public class AuthController : Controller
    {

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            var loginProviders = System.Web.HttpContext.Current.GetOwinContext().Authentication.GetAuthenticationTypes();

            var model = new SignInModel()
            {
                Action = "SignIn",
                ReturnUrl = "",
                Providers = loginProviders
            };
            
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("SignInConfirmation", "Auth", new { ReturnUrl = returnUrl }));
        }

        /// <summary>
        /// The actual callback which we get from GitHub's authentication.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> SignInConfirmation(string returnUrl)
        {
            // Get the information about the user from the external login provider
            var loginInfo = await HttpContext.GetOwinContext().Authentication.GetExternalLoginInfoAsync();

            if (loginInfo == null)
                return RedirectToAction("SignIn");

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Overview", "Issues");

            return RedirectToAction("Overview", "Issues");
        }

        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("SignIn");
        }

        /// <summary>
        /// Used for XSRF protection when adding external logins 
        /// </summary>
        private const string XsrfKey = "XsrfId";

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}