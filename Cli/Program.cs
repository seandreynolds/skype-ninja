using System;
using CommandLine;

namespace eigenein.SkypeNinja.Cli
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();
            CommandLineParser parser = new CommandLineParser(
                new CommandLineParserSettings(Console.Error));
            if (!parser.ParseArguments(args, options))
            {
                Environment.Exit(1);
            }
        }
    }
}
