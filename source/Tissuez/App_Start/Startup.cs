using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tissuez.Startup))]
namespace Tissuez
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}