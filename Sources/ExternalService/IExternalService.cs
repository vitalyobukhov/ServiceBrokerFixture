using System.ServiceModel;

namespace ExternalService
{
    // External service contract.
    [ServiceContract]
    interface IExternalService
    {
        // Process external request.
        [OperationContract]
        ProcessResponse Process(ProcessRequest request);
    }
}
