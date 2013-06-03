using System;
using System.Configuration;
using Common;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace ExternalService
{
    // External service hosting environment.
    class Program
    {
        private static ExternalServiceHost serviceHost;

        // Min value of random delay for external request execution.
        // Null - no delay.
        private static int? processMinDelay;

        // Max value of random delay for external request execution.
        // Null - no delay.
        private static int? processMaxDelay;


        // Gets external service url.
        private static string ServiceUri
        {
            get { return ConfigurationManager.AppSettings["ServiceUri"]; }
        }


        // Parses program startup arguments.
        private static void ParseArgs(string[] args)
        {
            if (args.Length >= 2)
            {
                int minTmp, maxTmp;

                // execution delays
                if (int.TryParse(args[0], out minTmp) && int.TryParse(args[1], out maxTmp) && 
                    minTmp >= 0 && maxTmp >= 0 && minTmp <= maxTmp)
                {
                    processMinDelay = minTmp;
                    processMaxDelay = maxTmp;
                }
            }
        }

        // Service host startup logic.
        private static void Listen()
        {
            var binding = new WSHttpBinding
            {
                MaxReceivedMessageSize = ushort.MaxValue,
                ReaderQuotas = { MaxStringContentLength = ushort.MaxValue}
            };

            var behavior = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            };

            serviceHost = new ExternalServiceHost(processMinDelay, processMaxDelay, typeof(ExternalService), new Uri(ServiceUri));
            serviceHost.AddServiceEndpoint(typeof(IExternalService), binding, string.Empty);
            serviceHost.Description.Behaviors.Add(behavior);
            serviceHost.Open();
        }

        // Disposes external service host in case of application termination.
        private static void Dispose()
        {
            if (serviceHost != null)
            {
                try { serviceHost.Close(); }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause
            }

            ConsoleInterop.ClearHandlers();
        }

        // Startup logic.
        private static void Main(string[] args)
        {
            Console.Title = "External Service";
            Console.WriteLine("External Service started.");

            // parse startup arguments
            ParseArgs(args);

            // try to start external service host
            try
            {
                Listen();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to start listener at {0}", ServiceUri);
                Console.WriteLine("Exception message: {0}", ex.Message);
                return;
            }

            // set disposal handler on application termination
            ConsoleInterop.SetHandler(Dispose);
            Console.WriteLine("External Service is listening at {0}", ServiceUri);
            Console.WriteLine("Press any key to quit.");

            // dispose and terminate application on key press
            Console.ReadKey();
            Dispose();
        }
    }
}
