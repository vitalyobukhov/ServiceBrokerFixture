using System;
using System.ServiceModel;

namespace ExternalService
{
    // Custom implementation of service host for external service
    // to pass delay arguments on instance construction.
    public class ExternalServiceHost : 
        ServiceHost
    {
        public ExternalServiceHost(int? minProcessDelay, int? maxProcessDelay, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            foreach (var cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new ExternalServiceInstanceProvider(minProcessDelay, maxProcessDelay));
            }
        }
    }
}
