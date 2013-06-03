using System;
using System.Threading.Tasks;
using Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;

namespace Producer
{
    // Incoming queue message producer.
    class Program
    {
        // Default params values (ms and bytes).
        private const int defaultBatchSize = 1;
        private const int defaultPayloadSize = 4096;
        private const int maxBatchSize = 1000000;
        private const int maxMessageDelay = 60000;
        private const int maxPayloadSize = 65536;


        // Program state.
        private static bool isClosing = false;
        private static readonly Random random = new Random();
        private static SqlConnection connection;

        // Min value of random delay between messages batch enqueue (ms).
        // Null - no delay.
        private static int? batchMinDelay;

        // Max value of random delay between messages batch enqueue (ms).
        // Null - no delay.
        private static int? batchMaxDelay;

        // Messages batch size between batch delay.
        private static int batchSize = defaultBatchSize;

        // Constant delay between messages in messages batch (ms).
        // Null - no delay.
        private static int? messageDelay;

        // Message payload (useful information) size (bytes).
        private static int payloadSize = defaultPayloadSize;


        // Gets connection string to sample service broker db.
        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ServiceBroker"].ConnectionString; }
        }


        // Creates and opens connection to sample service broker db.
        private static void OpenConnection()
        {
            if (connection == null)
                connection = new SqlConnection(ConnectionString);

            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        // Parses program startup arguments.
        private static void ParseArgs(string[] args)
        {
            int tmp, minTmp, maxTmp;

            // batch delays
            if (args.Length >= 2 && int.TryParse(args[0], out minTmp) && int.TryParse(args[1], out maxTmp) &&
                minTmp >= 0 && maxTmp >= 0 && minTmp <= maxTmp)
            {
                batchMinDelay = minTmp;
                batchMaxDelay = maxTmp;
            }

            // batch size
            if (args.Length >= 3 && int.TryParse(args[2], out tmp) && 
                tmp >= 1 && tmp <= maxBatchSize)
            {
                batchSize = tmp;
            }

            // message delay
            if (args.Length >= 4 && int.TryParse(args[3], out tmp) &&
                tmp > 0 && tmp <= maxMessageDelay)
            {
                messageDelay = tmp;
            }

            // payload size
            if (args.Length >= 5 && int.TryParse(args[4], out tmp) && 
                tmp >= 0 && tmp <= maxPayloadSize)
            {
                payloadSize = tmp;
            }
        }

        // Simulates delay between message production.
        private static void InnerProduceMessage()
        {
            if (messageDelay.HasValue)
                Thread.Sleep(messageDelay.Value);
        }

        // Simulates delay between messages batch production.
        private static void InnerProduceBatch()
        {
            if (batchMinDelay.HasValue && batchMaxDelay.HasValue)
                Thread.Sleep(random.Next(batchMinDelay.Value, batchMaxDelay.Value + 1));
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
                for (var i = 0; i < batchSize && !isClosing; i++)
                {
                    // create and serialize message for incoming message queue
                    var message = ProducerMessage.Generate(payloadSize);
                    var messageBody = message.ToString();

                    // enqueue message in incoming message queue
                    parameter.Value = messageBody;
                    command.ExecuteNonQuery();

                    Console.WriteLine("Enqueued message: {0}", message.Id);

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
        private static void Main(string[] args)
        {
            Console.Title = "Producer";
            Console.WriteLine("Producer started.");

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
            ConsoleInterop.SetHandler(Dispose);

            // parse startup arguments
            ParseArgs(args);

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
