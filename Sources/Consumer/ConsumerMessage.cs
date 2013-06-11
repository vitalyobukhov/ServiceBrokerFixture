using System;
using Common;

namespace Consumer
{
    // Message which is dequeued by consumer from outgoing message queue.
    public class ConsumerMessage
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

        // Timestamp of external service request receipt from mapper.
        public long ServiceReceived { get; set; }

        // Timestamp of external service request send to mapper.
        public long ServiceSent { get; set; }

        // Timestamp of external service response receipt by mapper.
        public long ServiceResponded { get; set; }

        // Timestamp of message receipt by consumer.
        public long Consumed { get; set;}


        // Deserializes message for outgoing message queue.
        public static ConsumerMessage FromString(string messageBody, Action<ConsumerMessage> initializer = null)
        {
            var result = MessageSerializer.Deserialize<ConsumerMessage>(messageBody, "OutMessage");

            if (initializer != null) initializer(result);

            return result;
        }
    }
}
