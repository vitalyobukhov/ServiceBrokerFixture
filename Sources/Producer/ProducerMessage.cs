using System;
using Common;

namespace Producer
{
    // Message which is enqueued to incoming message queue by producer.
    public class ProducerMessage
    {
        private static readonly Lazy<Random> random = new Lazy<Random>(); 


        // Id should be used by producer & consumer.
        public Guid Id { get; set; }

        // Some useful data.
        public string Payload { get; set; }

        // Timestamp of message production by producer.
        public long Produced { get; set; }



        // Generates payload (useful message data) in form of random string.
        private static string GeneratePayload(int payloadSize)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var buffer = new char[payloadSize];
            for (var i = 0; i < payloadSize; i++)
                buffer[i] = chars[random.Value.Next(chars.Length)];

            return new string(buffer);
        }

        // Creates producer message with id, payload and actual timestamp.
        public static ProducerMessage Generate(int payloadSize)
        {
            return new ProducerMessage
            {
                Id = Guid.NewGuid(),
                Payload = GeneratePayload(payloadSize),
                Produced = DateTime.Now.Ticks
            };
        }

        // Serializes message for incoming message queue.
        public override string ToString()
        {
            return MessageSerializer.Serialize(this, "InMessage");
        }
    }
}
