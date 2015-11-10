using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNet.SignalR.Messaging;

namespace Recipe51.MessageBus
{
    public class SignalRBackplaneConfiguration :
        ScaleoutConfiguration
    {
        public string EndpointAddress { get; set; }
    }
}
