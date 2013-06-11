using CommandLine;
using CommandLine.Text;

namespace Common
{
    // Program startup arguments base class.
    public abstract class ArgsBase
    {
        // Output processing info into console.
        [Option("verbose", DefaultValue = true, Required = false,
            HelpText = "Output processing info into console.")]
        public bool Verbose { get; set; }


        // Format help.
        [HelpOption]
        public string GetUsage()
        {
            var result = new HelpText();
            result.AddOptions(this);
            return result;
        }
    }
}
