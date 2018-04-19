using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shanghai.WebApp.Startup))]
namespace Shanghai.WebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
