using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using HotPotato;

[assembly: OwinStartup(typeof(HotPotato2.Startup))]

namespace HotPotato2
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
