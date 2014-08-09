using System;
//using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Providers.GitHub;
using Owin.Security.Providers.GooglePlus;
using Owin.Security.Providers.GooglePlus.Provider;
using Owin.Security.Providers.Instagram;
using Owin.Security.Providers.LinkedIn;
using Owin.Security.Providers.Salesforce;
using Owin.Security.Providers.StackExchange;
using Owin.Security.Providers.Yahoo;
using Owin.Security.Providers.OpenID;
using Owin.Security.Providers.Steam;
using Microsoft.AspNet.Identity;

namespace Tissuez
{
    public partial class Startup
    {
        private const string _ClientId = "YOUR_GITHUB_CLIENTID";
        private const string _ClientSecret = "YOUR_GITHUB_CLIENTSECRET";

        public void ConfigureAuth(IAppBuilder app)
        {
            // OWIN requires us to provide a default authentication type, before we setup an explicit one
            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);

            // Register GitHub's OAuth authentication
            app.UseGitHubAuthentication(
                clientId: _ClientId, 
                clientSecret: _ClientSecret
            );

        }
    }
}