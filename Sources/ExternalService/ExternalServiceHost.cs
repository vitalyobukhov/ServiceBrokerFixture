using System;
using System.ServiceModel;

namespace ExternalService
{
    // Custom implementation of service host for external service
    // to pass delay arguments on instance construction.
    class ExternalServiceHost : ServiceHost
    {
        public ExternalServiceHost(Args args, Type serviceType, params Uri[] baseAddresses) 
            : base(serviceType, baseAddresses)
        {
            foreach (var cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new ExternalServiceInstanceProvider(args));
            }
        }
    }
}
