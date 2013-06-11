using System;
using System.Configuration;
using CommandLine;
using Common;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace ExternalService
{
    // External service hosting environment.
    class Program
    {
        private static readonly Args args = new Args();
        private static ExternalServiceHost serviceHost;


        // Gets external service url.
        private static string ServiceUri
        {
            get { return ConfigurationManager.AppSettings["ServiceUri"]; }
        }


        // Service host startup logic.
        private static void Listen()
        {
            const int maxReceivedMessageSize = ushort.MaxValue;
            const int maxStringContentLength = ushort.MaxValue;

            var binding = new WSHttpBinding
            {
                MaxReceivedMessageSize = maxReceivedMessageSize,
                ReaderQuotas = { MaxStringContentLength = maxStringContentLength }
            };

            var behavior = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            };

            serviceHost = new ExternalServiceHost(args, typeof(ExternalService), new Uri(ServiceUri));
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
        private static void Main(string[] arguments)
        {
            Console.Title = "External Service";

            // parse startup arguments
            if (!Parser.Default.ParseArguments(arguments, args))
            {
                Console.WriteLine(new CommandLine.Text.HelpText());
                return;
            }

            Console.WriteLine("External Service started.");

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
