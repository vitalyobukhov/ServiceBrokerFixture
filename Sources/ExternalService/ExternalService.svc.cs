using System;
using System.Threading;

namespace ExternalService
{
    // External service implementation.
    class ExternalService : IExternalService
    {
        private readonly Args args;


        public ExternalService(Args args)
        {
            this.args = args;
        }


        // Simulates external execution delay.
        private void InnerProccess()
        {
            if (args.HasProcessDelay)
                Thread.Sleep(ThreadRandom.Next(args.ProcessMinDelay, args.ProcessMaxDelay + 1));
        }

        // Main request execution logic.
        public ProcessResponse Process(ProcessRequest request)
        {
            // set actual timestamp
            var serviceReceived = DateTime.Now.Ticks;

            // create response
            if (args.Verbose) Console.WriteLine("Requested message: {0}", request.Id);
            var response = new ProcessResponse(request) { ServiceReceived = serviceReceived };

            // simulation execution
            InnerProccess();

            // set actual timestamp
            response.ServiceSent = DateTime.Now.Ticks;
            if (args.Verbose) Console.WriteLine("Responded message: {0}", request.Id);

            return response;
        }
    }
}
