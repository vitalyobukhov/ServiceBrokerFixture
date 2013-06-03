using System;
using Common;

namespace Mapper
{
    // Message which is enqueued by mapper to outgoing message queue.
    public partial class MapperOutMessage
    {
        // Id should be used by producer & consumer.
        public Guid Id { get; set; }

        // Some useful data.
        public string Payload { get; set; }

        // Timestamp of message production by producer.
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

        // Timestamp of external service request receipt from mapper.
        public long ServiceReceived { get; set; }

        // Timestamp of external service request send to mapper.
        public long ServiceSent { get; set; }

        // Timestamp of external service response receipt by mapper.
        public long ServiceResponded { get; set; }


        // Serializes message for outgoing message queue.
        public override string ToString()
        {
            return MessageSerializer.Serialize(this, "OutMessage");
        }
    }
}
