using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Soulstone.Relay.Startup))]
namespace Soulstone.Relay
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}