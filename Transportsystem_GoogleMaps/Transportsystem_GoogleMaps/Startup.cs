using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Transportsystem_GoogleMaps.Startup))]
namespace Transportsystem_GoogleMaps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
