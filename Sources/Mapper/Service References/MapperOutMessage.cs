using Mapper.ExternalServiceReference;

namespace Mapper
{
    // Extensions of message which is enqueued by mapper to outgoing message queue.
    public partial class MapperOutMessage
    {
        public MapperOutMessage()
        { }

        public MapperOutMessage(ProcessResponse response)
        {
            Id = response.Id;
            Payload = response.Payload;
            Produced = response.Produced;
            MapperActivated = response.MapperActivated;
            MapperConnected = response.MapperConnected;
            MapperPreDequeued = response.MapperPreDequeued;
            MapperPostDequeued = response.MapperPostDequeued;
            MapperReceived = response.MapperReceived;
            MapperSent = response.MapperSent;
            ServiceReceived = response.ServiceReceived;
            ServiceSent = response.ServiceSent;
        } 
    }
}
