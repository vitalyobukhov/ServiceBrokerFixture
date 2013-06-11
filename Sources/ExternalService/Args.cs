using CommandLine;
using Common;

namespace ExternalService
{
    class Args : ArgsBase
    {
        // Min value of random delay for external request execution.
        [Option('l', "mindelay", DefaultValue = 0, Required = false,
            HelpText = "Min value of random delay for external request execution.")]
        public int ProcessMinDelay { get; set; }

        // Max value of random delay for external request execution.
        [Option('u', "maxdelay", DefaultValue = 0, Required = false,
            HelpText = "Max value of random delay for external request execution.")]
        public int ProcessMaxDelay { get; set; }


        // Determines whenever arguments contain process delay.
        public bool HasProcessDelay
        {
            get { return ProcessMinDelay >= 0 && ProcessMaxDelay > 0 && ProcessMinDelay <= ProcessMaxDelay; }
        }
    }
}
