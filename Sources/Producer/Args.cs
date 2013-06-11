using CommandLine;
using Common;

namespace Producer
{
    class Args : ArgsBase
    {
        // Min value of random delay between messages batch enqueue (ms).
        [Option('l', "mindelay", DefaultValue = 0, Required = false,
            HelpText = "Min value of random delay between messages batch enqueue (ms).")]
        public int BatchMinDelay { get; set; }

        // Max value of random delay between messages batch enqueue (ms).
        [Option('u', "maxdelay", DefaultValue = 0, Required = false,
            HelpText = "Max value of random delay between messages batch enqueue (ms).")]
        public int BatchMaxDelay { get; set; }

        // Messages batch size between batch delay.
        [Option('b', "batch", DefaultValue = 1, Required = false,
            HelpText = "Messages batch size between batch delay.")]
        public int BatchSize { get; set; }

        // Constant delay between messages in messages batch (ms).
        [Option('d', "delay", DefaultValue = 0, Required = false,
            HelpText = "Constant delay between messages in messages batch (ms).")]
        public int MessageDelay { get; set; }

        // Message payload (useful information) size (bytes).
        [Option('p', "payload", DefaultValue = 4096, Required = false,
            HelpText = "Message payload (useful information) size (bytes).")]
        public int PayloadSize { get; set; }


        // Determines whenever arguments contain batch delay.
        public bool HasBatchDelay
        {
            get { return BatchMinDelay >= 0 && BatchMaxDelay > 0 && BatchMinDelay <= BatchMaxDelay; }
        }

        // Determines whenever arguments contain message delay.
        public bool HasMessageDelay
        {
            get { return MessageDelay > 0; }
        }
    }
}
