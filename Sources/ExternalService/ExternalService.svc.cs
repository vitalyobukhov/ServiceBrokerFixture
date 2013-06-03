using System;
using System.Threading;

namespace ExternalService
{
    // External service implementation.
    public class ExternalService : 
        IExternalService
    {
        // Min value of random delay for external request execution.
        // Null - no delay.
        private readonly int? minProcessDelay;

        // Max value of random delay for external request execution.
        // Null - no delay.
        private readonly int? maxProcessDelay;


        public ExternalService(int? minProcessDelay, int? maxProcessDelay)
        {
            if (minProcessDelay.HasValue && maxProcessDelay.HasValue &&
                minProcessDelay.Value >= 0 && maxProcessDelay.Value >= 0 && 
                minProcessDelay.Value <= maxProcessDelay.Value)
            {
                this.minProcessDelay = minProcessDelay;
                this.maxProcessDelay = maxProcessDelay;
            }
        }


        // Simulates external execution delay.
        private void InnerProccess()
        {
            if (minProcessDelay.HasValue && maxProcessDelay.HasValue)
                Thread.Sleep(ThreadRandom.Next(minProcessDelay.Value, maxProcessDelay.Value + 1));
        }

        // Main request execution logic.
        public ProcessResponse Process(ProcessRequest request)
        {
            // set actual timestamp
            var serviceReceived = DateTime.Now.Ticks;

            Console.WriteLine("Requested message: {0}", request.Id);
            var response = new ProcessResponse(request) { ServiceReceived = serviceReceived };

            // simulation execution
            InnerProccess();

            // set actual timestamp
            response.ServiceSent = DateTime.Now.Ticks;
            Console.WriteLine("Responded message: {0}", request.Id);

            return response;
        }
    }
}
