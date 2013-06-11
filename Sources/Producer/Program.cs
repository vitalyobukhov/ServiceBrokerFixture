using System;
using System.Threading.Tasks;
using Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;
using CommandLine;

namespace Producer
{
    // Incoming queue message producer.
    class Program
    {
        // Program state.
        private static bool isClosing;
        private static readonly Random random = new Random();
        private static SqlConnection connection;
        private static readonly Args args = new Args();


        // Gets connection string to sample service broker db.
        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ServiceBroker"].ConnectionString; }
        }


        // Creates and opens connection to sample service broker db.
        private static void OpenConnection()
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
        }

        // Simulates delay between message production.
        private static void InnerProduceMessage()
        {
            if (args.HasMessageDelay)
                Thread.Sleep(args.MessageDelay);
        }

        // Simulates delay between messages batch production.
        private static void InnerProduceBatch()
        {
            if (args.HasBatchDelay)
                Thread.Sleep(random.Next(args.BatchMinDelay, args.BatchMaxDelay + 1));
        }

        // Main production logic.
        private static void Produce()
        {
            // prepare call of sp to enqueue message in incoming message queue
            var command = new SqlCommand("EnqueueIn", connection) { CommandType = CommandType.StoredProcedure };
            var parameter = new SqlParameter("@messageBody", SqlDbType.Xml);
            command.Parameters.Add(parameter);

            // should exit on application termination 
            while (!isClosing)
            {
                // batch loop
                for (var i = 0; i < args.BatchSize && !isClosing; i++)
                {
                    // create and serialize message for incoming message queue
                    var message = ProducerMessage.Generate(args.PayloadSize);
                    var messageBody = message.ToString();

                    // enqueue message in incoming message queue
                    parameter.Value = messageBody;
                    command.ExecuteNonQuery();

                    if (args.Verbose) Console.WriteLine("Enqueued message: {0}", message.Id);

                    // simulate delay between messages
                    InnerProduceMessage();
                }

                // simulate delay between batches
                InnerProduceBatch();
            }
        }

        // Disposes connection to db in case of application termination.
        private static void Dispose()
        {
            isClosing = true;

            if (connection != null)
            {
                try { connection.Close(); }
                catch { }
            }

            ConsoleInterop.ClearHandlers();
        }

        // Startup logic.
        private static void Main(string[] arguments)
        {
            Console.Title = "Producer";

            // parse startup arguments
            if (!Parser.Default.ParseArguments(arguments, args))
            {
                Console.WriteLine(new CommandLine.Text.HelpText());
                return;
            }

            Console.Write("Producer started.");

            // try to open permanent db connection for application lifecycle
            try
            {
                OpenConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to open connection via {0}", ConnectionString);
                Console.WriteLine("Exception message: {0}", ex.Message);
                return;
            }

            // set disposal handler on application termination
            isClosing = false;
            ConsoleInterop.SetHandler(Dispose);

            // start main logic in separate thread
            Task.Factory.StartNew(Produce);
            Console.WriteLine("Producer connected via {0}", ConnectionString);
            Console.WriteLine("Press any key to quit.");

            // dispose and terminate application on key press
            Console.ReadKey();
            Dispose();
        }
    }
}
