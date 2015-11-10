using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using Recipe51.MessageBus;

[assembly: OwinStartup(typeof(Recipe51.Startup))]

namespace Recipe51
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.UseSignalRBackplane("http://localhost:7526");
            app.MapSignalR();
        }
    }
}
