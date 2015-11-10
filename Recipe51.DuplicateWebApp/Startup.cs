using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Recipe51.MessageBus;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(Recipe51.DuplicateWebApp.Startup))]

namespace Recipe51.DuplicateWebApp
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
