using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNet.SignalR.Messaging;

namespace Recipe51.MessageBus
{
    public class SignalRBackplaneMessage
    {
        public ulong Id
        {
            get;
            private set;
        }
        public ScaleoutMessage ScaleoutMessage
        {
            get;
            private set;
        }
        public static byte[] ToBytes(
            long id, IList<Message> messages)
        {
            if (messages == null)
            {
                throw new
                    ArgumentNullException("messages");
            }

            using (var ms = new MemoryStream())
            {
                var binaryWriter = new BinaryWriter(ms);

                var scaleoutMessage = new
                    ScaleoutMessage(messages);
                var buffer = scaleoutMessage.ToBytes();

                binaryWriter.Write(id);
                binaryWriter.Write(buffer.Length);
                binaryWriter.Write(buffer);

                return ms.ToArray();
            }
        }

        public static SignalRBackplaneMessage FromBytes(
            byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var binaryReader = new
                    BinaryReader(stream);
                var id = (ulong)binaryReader.ReadInt64();
                var count = binaryReader.ReadInt32();
                var buffer = binaryReader.ReadBytes(count);

                return new SignalRBackplaneMessage
                {
                    Id = id,
                    ScaleoutMessage =
                        ScaleoutMessage.FromBytes(buffer)
                };
            }
        }
    }
}
