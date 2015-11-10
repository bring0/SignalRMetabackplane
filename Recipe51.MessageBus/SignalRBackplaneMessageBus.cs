using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Messaging;

namespace Recipe51.MessageBus
{
    class SignalRBackplaneMessageBus : ScaleoutMessageBus
    {
        private readonly HubConnection _connection;
        private readonly IHubProxy _hub;

        public SignalRBackplaneMessageBus(IDependencyResolver dependencyResolver,
        SignalRBackplaneConfiguration configuration) : base(dependencyResolver, configuration)
        {
            _connection = new HubConnection(
            configuration.EndpointAddress);

            _hub = _connection.CreateHubProxy("backplane");

            _hub.On<byte[]>("Broadcast", m =>
            {
                var message = SignalRBackplaneMessage.FromBytes(m);
                OnReceived(0,
                message.Id, message.ScaleoutMessage);
            });

            _connection.Start().Wait();
        }
        protected override Task Send(
        int streamIndex, IList<Message> messages)
        {
            return Send(messages);
        }

        protected override Task Send(IList<Message> messages)
        {
            if (_connection.State != ConnectionState.Connected) return Task.FromResult(false);

            var newId = _hub.Invoke<long>("GetId").Result;

            var data = SignalRBackplaneMessage.ToBytes(newId, messages);

            return _hub.Invoke("Publish", data);
        }
    }
}