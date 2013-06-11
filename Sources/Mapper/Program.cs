using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using CommandLine;
using Mapper.ExternalServiceReference;

namespace Mapper
{
    // Main solution logic.
    // Mapper from incoming message with external service invocation to outgoing message
    class Program
    {
        private static readonly Random random = new Random();
        private static readonly Args args = new Args();


        // Gets external service url.
        private static string ServiceUri
        {
            get { return ConfigurationManager.AppSettings["ServiceUri"]; }
        }

        // Gets connection string to sample service broker db.
        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ServiceBroker"].ConnectionString; }
        }

        // Simulates map execution delay.
        private static void InnerMap()
        {
            if (args.HasMapDelay)
                Thread.Sleep(random.Next(args.MapMinDelay, args.MapMaxDelay + 1));
        }

        // Simulates incoming map execution delay.
        private static ProcessRequest InnerMapIn(MapperInMessage message)
        {
            // set actual timestamp
            message.MapperSent = DateTime.Now.Ticks;

            // convert incoming message to external service request
            var request = new ProcessRequest(message);
            
            // delay
            InnerMap();

            return request;
        }

        // Simulates outgoing map execution delay.
        private static MapperOutMessage InnerMapOut(ProcessResponse response)
        {
            // set actual timestamp
            var externalServiceResponded = DateTime.Now.Ticks;

            // convert external service response to outgoing message
            var message = new MapperOutMessage(response) { ServiceResponded = externalServiceResponded };

            // delay
            InnerMap();

            return message;
        }

        // Main execution logic.
        private static void Map()
        {
            // set actual timestamp
            var mapperActivated = DateTime.Now.Ticks;

            // try to open connection to external service
            using (var serviceClient = new ExternalServiceClient("ExternalService", ServiceUri))
            {
                serviceClient.Open();

                // try to open connection to service broker sample db
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    //set actual timestamp
                    var mapperConnected = DateTime.Now.Ticks;

                    // prepare call of sp to dequeue message to outgoing message queue
                    var dequeueCommand = new SqlCommand("DequeueIn", connection) { CommandType = CommandType.StoredProcedure };
                    dequeueCommand.Parameters.AddWithValue("@batchSize", args.BatchSize);
                    var dequeueCommandBatchId = new SqlParameter("@batchId", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output };
                    dequeueCommand.Parameters.Add(dequeueCommandBatchId);

                    // prepare call of tvl to get messages from incoming message table
                    var getCommand = new SqlCommand("SELECT * FROM GetIn(@batchId)", connection);
                    var getCommandBatchId = new SqlParameter("@batchId", SqlDbType.UniqueIdentifier);
                    getCommand.Parameters.Add(getCommandBatchId);

                    // prepare call of sp to enqueue message from incoming message queue
                    var enqueueCommand = new SqlCommand("EnqueueOut", connection) { CommandType = CommandType.StoredProcedure };
                    var enqueueCommandMessageBody = new SqlParameter("@messageBody", SqlDbType.Xml);
                    enqueueCommand.Parameters.Add(enqueueCommandMessageBody);

                    // prepare call of so to delete message from incoming message table
                    var truncateCommand = new SqlCommand("TruncateIn", connection) { CommandType = CommandType.StoredProcedure };
                    var truncateCommandBatchId = new SqlParameter("@batchId", SqlDbType.UniqueIdentifier);
                    truncateCommand.Parameters.Add(truncateCommandBatchId);

                    // main messages handling loop
                    while (true)
                    {
                        //set actual timestamp
                        var mapperPreDequeued = DateTime.Now.Ticks;

                        // no messages
                        if ((int)dequeueCommand.ExecuteScalar() == 0)
                            break;

                        // get batch id as output parameter
                        var batchId = (Guid)dequeueCommandBatchId.Value;
                        getCommandBatchId.Value = batchId;
                        truncateCommandBatchId.Value = batchId;

                        // get messages from incoming table
                        using (var inTable = new DataTable())
                        {
                            using (var adapter = new SqlDataAdapter(getCommand))
                            {
                                adapter.Fill(inTable);
                            }
                                
                            //set actual timestamp
                            var mapperPostDequeued = DateTime.Now.Ticks;

                            // handle incoming messages batch
                            foreach (DataRow row in inTable.Rows)
                            {
                                // get incoming message body
                                var mapperInMessageBody = (string)row["MessageBody"];

                                // set actual timestamp
                                var mapperReceived = DateTime.Now.Ticks;

                                // deserialize incoming message
                                var inMessage = MapperInMessage.FromString(mapperInMessageBody, m =>
                                {
                                    m.MapperActivated = mapperActivated;
                                    m.MapperConnected = mapperConnected;
                                    m.MapperPreDequeued = mapperPreDequeued;
                                    m.MapperPostDequeued = mapperPostDequeued;
                                    m.MapperReceived = mapperReceived;
                                });
                                if (args.Verbose) Console.WriteLine("Dequeued message: {0}", inMessage.Id);

                                // simulate map execution
                                var externalServiceRequest = InnerMapIn(inMessage);

                                // invoke external request
                                if (args.Verbose) Console.WriteLine("Sent message:     {0}", externalServiceRequest.Id);
                                var externalServiceResponse = serviceClient.Process(externalServiceRequest);
                                if (args.Verbose) Console.WriteLine("Received message: {0}", externalServiceResponse.Id);

                                // convert external service response to outgoing message
                                var outMessage = InnerMapOut(externalServiceResponse);

                                // serialzie outgoing message
                                var mapperOutMessageBody = outMessage.ToString();

                                // enqueue message to outgoing message queue
                                enqueueCommandMessageBody.Value = mapperOutMessageBody;
                                enqueueCommand.ExecuteNonQuery();
                                if (args.Verbose) Console.WriteLine("Enqueued message: {0}", outMessage.Id);
                            }
                        }

                        // delete messages from incoming message table
                        truncateCommand.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                serviceClient.Close();
            }
        }

        // Startup logic.
        private static void Main(string[] arguments)
        {
            // parse startup arguments
            if (!Parser.Default.ParseArguments(arguments, args))
            {
                Console.WriteLine(new CommandLine.Text.HelpText());
                return;
            }

            // execute main logic
            Map();
        }
    }
}
