using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(HotPotato.Startup))]

namespace HotPotato
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(typeof(PotatoHub), () => new PotatoHub(new PotatoManager()));

            app.MapSignalR();
        }
    }
}
