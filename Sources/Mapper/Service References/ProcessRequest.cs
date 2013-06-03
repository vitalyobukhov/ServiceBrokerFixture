namespace Mapper.ExternalServiceReference
{
    // Extensions of external service request wrapper.
    public partial class ProcessRequest
    {
        public ProcessRequest()
        { }

        public ProcessRequest(MapperInMessage message)
        {
            Id = message.Id;
            Payload = message.Payload;
            Produced = message.Produced;
            MapperActivated = message.MapperActivated;
            MapperConnected = message.MapperConnected;
            MapperPreDequeued = message.MapperPreDequeued;
            MapperPostDequeued = message.MapperPostDequeued;
            MapperReceived = message.MapperReceived;
            MapperSent = message.MapperSent;
        }
    }
}
