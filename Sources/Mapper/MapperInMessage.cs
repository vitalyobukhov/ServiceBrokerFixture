using System;
using Common;

namespace Mapper
{
    // Message which is dequeued by mapper from incoming message queue.
    public class MapperInMessage
    {
        // Id should be used by producer & consumer.
        public Guid Id { get; set; }

        // Some useful data.
        public string Payload { get; set; }

        // Timestamp of message creation by producer.
        public long Produced { get; set; }

        // Timestamp of mapper activation by ext activator via incoming message queue.
        public long MapperActivated { get; set; }

        // Timestamp of mapper connection establishment with db & external service.
        public long MapperConnected { get; set; }

        // Timestamp of mapper preparation to get messages from incoming message queue.
        public long MapperPreDequeued { get; set; }

        // Timestamp of mapper completion to get messages from incoming message queue.
        public long MapperPostDequeued { get; set; }

        // Timestamp of mapper message receipt from incoming message queue.
        public long MapperReceived { get; set; }

        // Timestamp of mapper request send to external service.
        public long MapperSent { get; set; }


        // Deserializes message for incoming message queue.
        public static MapperInMessage FromString(string messageBody, Action<MapperInMessage> finalizer = null)
        {
            var result = MessageSerializer.Deserialize<MapperInMessage>(messageBody, "InMessage");

            if (finalizer != null) finalizer(result);

            return result;
        }
    }
}
