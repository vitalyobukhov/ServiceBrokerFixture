using CommandLine;
using Common;

namespace Consumer
{
    class Args : ArgsBase
    {
        // Messages batch size between batch delay.
        [Option('b', "batch", DefaultValue = 0, Required = false,
            HelpText = "Messages batch size between batch delay.")]
        public int BatchSize { get; set; }


        // Determines whenever arguments contain statistic flag.
        public bool HasMeasures
        {
            get { return BatchSize > 0; }
        }
    }
}
