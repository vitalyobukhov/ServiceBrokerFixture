namespace Consumer
{
    // Fixture execution measurement.
    struct Measure
    {
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
        public long Consumed { get; set; }


        public Measure(ConsumerMessage message) : this()
        {
            Produced = message.Produced;
            MapperActivated = message.MapperActivated;
            MapperConnected = message.MapperConnected;
            MapperPreDequeued = message.MapperPreDequeued;
            MapperPostDequeued = message.MapperPostDequeued;
            MapperReceived = message.MapperReceived;
            MapperSent = message.MapperSent;
            ServiceReceived = message.ServiceReceived;
            ServiceSent = message.ServiceSent;
            ServiceResponded = message.ServiceResponded;
            Consumed = message.Consumed;
        }
    }
}
