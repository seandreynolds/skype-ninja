﻿using System;

using CommandLine;
using CommandLine.Text;

namespace eigenein.SkypeNinja.Cli.Common
{
    /// <summary>
    /// Represents the application options.
    /// </summary>
    internal class Options
    {
        [Option(
            's', 
            "source-uri", 
            HelpText="Source storage URI.",
            Required=true)]
        public string SourceUriString
        {
            get;
            set;
        }

        [Option(
            't', 
            "target-uri", 
            HelpText="Target storage URI.",
            Required=true)]
        public string TargetUriString
        {
            get;
            set;
        }

        [Option(
            "list-schemes",
            HelpText = "List source and target schemes and exit.")]
        public bool ListSchemes
        {
            get;
            set;
        }

        [HelpOption(
            'h', 
            "help", 
            HelpText="Show the help and exit.")]
        public string GetUsage()
        {
            return HelpText.AutoBuild(
                this, 
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
