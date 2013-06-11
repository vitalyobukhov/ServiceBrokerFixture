using CommandLine;
using Common;

namespace Mapper
{
    class Args : ArgsBase
    {
        // Min value of random delay for map execution (ms).
        [Option('l', "mindelay", DefaultValue = 0, Required = false,
            HelpText = "Min value of random delay for map execution (ms).")]
        public int MapMinDelay { get; set; }

        // Max value of random delay for map execution.
        [Option('u', "maxdelay", DefaultValue = 0, Required = false,
            HelpText = "Max value of random delay for map execution (ms).")]
        public int MapMaxDelay { get; set; }

        // Batch size for incoming message queue.
        [Option('b', "batch", DefaultValue = 100, Required = false,
            HelpText = "Batch size for incoming message queue.")]
        public int BatchSize { get; set; }


        // Determines whenever arguments contain map delay.
        public bool HasMapDelay
        {
            get { return MapMinDelay >= 0 && MapMaxDelay > 0 && MapMinDelay <= MapMaxDelay; }
        }
    }
}
