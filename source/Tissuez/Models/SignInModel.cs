using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tissuez.Models
{
    public class SignInModel
    {
        public IEnumerable<AuthenticationDescription> Providers { get; set; }
        public string Action { get; set; }
        public string ReturnUrl { get; set; }

        public SignInModel()
        {
            Providers = new List<AuthenticationDescription>();
        }

    }
}