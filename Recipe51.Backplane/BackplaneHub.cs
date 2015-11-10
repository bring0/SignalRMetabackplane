using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Recipe51.Backplane
{
    [HubName("backplane")]
    public class BackplaneHub : Hub
    {
        private static long _id = 0;
        private static readonly object Locker = new object();

        public void Publish(byte[] message)
        {
            Clients.All.Broadcast(message);
        }

        public long GetId()
        {
            lock (Locker)
            {
                return ++_id;
            }
        }
    }
}