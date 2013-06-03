using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ExternalService
{
    // Custom implementation of instance provider for external service
    // to pass delay arguments on instance construction.
    public class ExternalServiceInstanceProvider :
        IInstanceProvider,
        IContractBehavior
    {
        private readonly int? minProcessDelay;
        private readonly int? maxProcessDelay;


        public ExternalServiceInstanceProvider(int? minProcessDelay, int? maxProcessDelay)
        {
            this.minProcessDelay = minProcessDelay;
            this.maxProcessDelay = maxProcessDelay;
        }


        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return new ExternalService(minProcessDelay, maxProcessDelay);
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance) { }
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint) { }
    }
}
