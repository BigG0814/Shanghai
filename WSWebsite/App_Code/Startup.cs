using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WSWebsite.Startup))]
namespace WSWebsite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
