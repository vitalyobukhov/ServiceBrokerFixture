using System;
using System.Runtime.Serialization;

namespace ExternalService
{
    // Response which is sent by external service to mapper.
    [DataContract]
    class ProcessResponse
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

        // Timestamp of external service request receipt from mapper.
        [DataMember]
        public long ServiceReceived { get; set; }

        // Timestamp of external service request send to mapper.
        [DataMember]
        public long ServiceSent { get; set; }


        public ProcessResponse()
        { }

        public ProcessResponse(ProcessRequest request)
        {
            Id = request.Id;
            Payload = request.Payload;
            Produced = request.Produced;
            MapperActivated = request.MapperActivated;
            MapperConnected = request.MapperConnected;
            MapperPreDequeued = request.MapperPreDequeued;
            MapperPostDequeued = request.MapperPostDequeued;
            MapperReceived = request.MapperReceived;
            MapperSent = request.MapperSent;
        }
    }
}
