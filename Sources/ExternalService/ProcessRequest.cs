using System;
using System.Runtime.Serialization;

namespace ExternalService
{
    // Request which is received by external service from mapper.
    [DataContract]
    class ProcessRequest
    {
        // Id should be used by producer & consumer.
        [DataMember]
        public Guid Id { get; set; }

        // Some useful data.
        [DataMember]
        public string Payload { get; set; }

        // Timestamp of message creation by producer.
        [DataMember]
        public long Produced { get; set; }

        // Timestamp of mapper activation by ext activator via incoming message queue.
        [DataMember]
        public long MapperActivated { get; set; }

        // Timestamp of mapper connection establishment with db & external service.
        [DataMember]
        public long MapperConnected { get; set; }

        // Timestamp of mapper preparation to get messages from incoming message queue.
        [DataMember]
        public long MapperPreDequeued { get; set; }

        // Timestamp of mapper completion to get messages from incoming message queue.
        [DataMember]
        public long MapperPostDequeued { get; set; }

        // Timestamp of mapper message receipt from incoming message queue.
        [DataMember]
        public long MapperReceived { get; set; }

        // Timestamp of mapper request send to external service.
        [DataMember]
        public long MapperSent { get; set; }
    }
}
