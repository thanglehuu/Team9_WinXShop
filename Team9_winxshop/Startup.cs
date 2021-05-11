using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Team9_winxshop.Startup))]
namespace Team9_winxshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
