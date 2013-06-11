using CommandLine;
using Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Consumer
{
    // Outgoing queue message consumer.
    class Program
    {
        // Listen command message body parameter name.
        private const string listenCommandMessageBodyName = "@messageBody";


        private static SqlConnection connection;
        private static readonly Args args = new Args();

        // Currect sequence number of consumed message.
        private static int measureIndex;

        // Consumed messages measures holder.
        private static Measure[] measures;


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

        // Prints statistic for consumed messages batch
        // with given title and subtraction function.
        private static void PrintMeasure(string title, Func<Measure, long> diff)
        {
            const string measureFormat = "000000";

            Console.WriteLine("{0} = {1} | {2} | {3} ", title,
            TimeSpan.FromTicks(Math.Max(measures.Min(diff), 0)).TotalMilliseconds.ToString(measureFormat),
            TimeSpan.FromTicks((long)Math.Max(measures.Average(diff), 0)).TotalMilliseconds.ToString(measureFormat),
            TimeSpan.FromTicks(Math.Max(measures.Max(diff), 0)).TotalMilliseconds.ToString(measureFormat));
        }

        // Prints statistic for entire consumed messages batch.
        private static void PrintMeasures()
        {
            PrintMeasure("Mapper Activated    - Produced          ", m => m.MapperActivated    - m.Produced);
            PrintMeasure("Mapper Connected    - Mapper Activated  ", m => m.MapperConnected    - m.MapperActivated);
            PrintMeasure("Mapper Postdequeued - Mapper Predequeued", m => m.MapperPostDequeued - m.MapperPreDequeued);
            PrintMeasure("Mapper Sent         - Mapper Received   ", m => m.MapperSent         - m.MapperReceived);
            PrintMeasure("Service Received    - Mapper Sent       ", m => m.ServiceReceived    - m.MapperSent);
            PrintMeasure("Service Sent        - Service Received  ", m => m.ServiceSent        - m.ServiceReceived);
            PrintMeasure("Service Responded   - Service Sent      ", m => m.ServiceResponded   - m.ServiceSent);
            PrintMeasure("Consumed            - Service Responded ", m => m.Consumed           - m.ServiceResponded);
            PrintMeasure("Consumed            - Mapper Received   ", m => m.Consumed           - m.MapperReceived);
            PrintMeasure("Consumed            - Produced          ", m => m.Consumed           - m.Produced);
        }

        // Complete listening on outgoing message queue.
        private static void OnConsumeComplete(IAsyncResult asynResult)
        {
            var command = (SqlCommand)asynResult.AsyncState;
            bool? listenResult = null;

            // get sp listen result
            // 0 - ended by timeout, no new messages
            // 1 - got new message
            // null - unsupported message type
            using (var reader = (command.EndExecuteReader(asynResult)))
            {
                if (reader.Read() && !Convert.IsDBNull(reader[0]))
                    listenResult = reader.GetInt32(0) == 1;
                reader.Close();
            }

            // not unsupported message type
            if (listenResult.HasValue)
            {
                // got new message
                if (listenResult.Value)
                {
                    // set actual timestamp
                    var consumed = DateTime.Now.Ticks;

                    // get outgoing message body as output parameter
                    var mapperOutMessageBody = (string)command.Parameters[listenCommandMessageBodyName].Value;

                    // deserialize outgoing message
                    var message = ConsumerMessage.FromString(mapperOutMessageBody, m => m.Consumed = consumed);
                    Console.WriteLine("Dequeued message: {0}", message.Id);

                    // count statistic
                    if (args.HasMeasures)
                    {
                        measures[measureIndex++] = new Measure(message);

                        // batch consumed
                        if (measureIndex >= args.BatchSize)
                        {
                            measureIndex = 0;
                            Console.WriteLine();
                            PrintMeasures();
                            Console.WriteLine();
                        }
                    }
                }
                // no new messages
                else
                {
                    Console.WriteLine("Queue is empty");
                }
            }

            Consume();
        }
        
        // Listen for outgoing message queue.
        private static void Consume()
        {
            // pooling timeouts
            const int listenCommandTimeoutValue = 10000;
            const int listenCommandTimeout = listenCommandTimeoutValue + 10000;

            // prepare call of sp to dequeue message from outgoing message queue
            var command = new SqlCommand("ListenOut", connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = listenCommandTimeout
            };

            command.Parameters.AddRange(new[] 
            {
                new SqlParameter("@timeout", SqlDbType.Int) { Value = listenCommandTimeoutValue },
                new SqlParameter(listenCommandMessageBodyName, SqlDbType.Xml) { Direction = ParameterDirection.Output }
            });

            // wait for outgoing message
            command.BeginExecuteReader(OnConsumeComplete, command);
        }

        // Disposes connection to db in case of application termination.
        private static void Dispose()
        {
            if (connection != null)
            {
                try { connection.Close(); }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause
            }

            ConsoleInterop.ClearHandlers();
        }

        // Startup logic.
        private static void Main(string[] arguments)
        {
            Console.Title = "Consumer";

            // parse startup arguments
            if (!Parser.Default.ParseArguments(arguments, args))
            {
                Console.WriteLine(new CommandLine.Text.HelpText());
                return;
            }

            // prepare measure vars
            if (args.HasMeasures)
            {
                measureIndex = 0;
                measures = new Measure[args.BatchSize];
            }

            Console.WriteLine("Consumer started.");

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

            // start consumer listener
            Consume();
            Console.WriteLine("Consumer connected via {0}", ConnectionString);
            Console.WriteLine("Press any key to quit.");

            // dispose and terminate application on key press
            Console.ReadKey();
            Dispose();
        }
    }
}
