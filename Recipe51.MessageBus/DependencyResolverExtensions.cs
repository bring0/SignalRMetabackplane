using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;

namespace Recipe51.MessageBus
{
    public static class DependencyResolverExtensions
    {
        public static void UseSignalRBackplane(
            this IDependencyResolver resolver,
            string endpointAddress)
        {
            resolver.UseSignalRBackplane(
                new SignalRBackplaneConfiguration
                {
                    EndpointAddress = endpointAddress
                });
        }

        public static void UseSignalRBackplane(
            this IDependencyResolver resolver,
            SignalRBackplaneConfiguration configuration)
        {
            resolver.Register(
                typeof(IMessageBus),
                () => new SignalRBackplaneMessageBus(
                    resolver,
                    configuration));
        }
    }
}
