using System;
using CommandLine;
using CommandLine.Text;

namespace eigenein.SkypeNinja.Cli
{
    internal class Options : CommandLineOptionsBase
    {
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(
                this, 
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
