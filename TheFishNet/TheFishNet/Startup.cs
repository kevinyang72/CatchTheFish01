using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheFishNet.Startup))]
namespace TheFishNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
