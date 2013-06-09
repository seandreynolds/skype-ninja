﻿using System;
using System.Collections.Generic;

using CommandLine;
using CommandLine.Text;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Common.Helpers;
using eigenein.SkypeNinja.Core.Connectors;
using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Cli.Common
{
    /// <summary>
    /// Represents the application options.
    /// </summary>
    internal class Options
    {
        private static readonly IDictionary<string, string> SchemeHelpStrings =
            new Dictionary<string, string>()
            {
                {ConnectorUriScheme.SkypeDb, Translator.GetString("Help.Schemes.SkypeDb")},
                {ConnectorUriScheme.SkypeId, Translator.GetString("Help.Schemes.SkypeId")},
                {ConnectorUriScheme.Json, Translator.GetString("Help.Schemes.Json")},
            };

        private static readonly IDictionary<string, string> GrouperHelpStrings =
            new Dictionary<string, string>()
            {
                {"author", Translator.GetString("Help.Groupers.Author")},
            };

        // ReSharper disable UnusedAutoPropertyAccessor.Global

        [Option(
            's', 
            "source-uri", 
            HelpText = "Source storage URI.",
            MetaValue = "URI",
            Required = true)]
        public string SourceUriString
        {
            get;
            set;
        }

        [Option(
            't', 
            "target-uri", 
            HelpText = "Target storage URI.",
            MetaValue = "URI",
            Required = true)]
        public string TargetUriString
        {
            get;
            set;
        }

        [Option(
            'f',
            "filter",
            HelpText = "Message filtering rule.",
            MetaValue = "FILTERS",
            Required = false)]
        public string Filters
        {
            get;
            set;
        }

        [Option(
            'g',
            "groupby",
            HelpText = "Comma-separated groupers.",
            MetaValue = "GROUPERS",
            Required = false)]
        public string Groups
        {
            get;
            set;
        }

        // ReSharper restore UnusedAutoPropertyAccessor.Global

        [HelpOption(
            'h', 
            "help", 
            HelpText="Show the help and exit.")]
        public string GetUsage()
        {
            // Build the standard help text.
            HelpText helpText = HelpText.AutoBuild(
                this, 
                current => HelpText.DefaultParsingErrorsHandler(this, current));
            // Add the schemes help.
            helpText.AddPostOptionsLine("Available source schemes:");
            AddSchemesHelp(helpText, UniversalConnectorFactory.SourceUriSchemes);
            helpText.AddPostOptionsLine("Available target schemes:");
            AddSchemesHelp(helpText, UniversalConnectorFactory.TargetUriSchemes);
            // Add the groupers help.
            helpText.AddPostOptionsLine("Available groupers:");
            AddGroupersHelp(helpText, GrouperCollection.AvailableGroupers);
            // Done.
            helpText.AddPostOptionsLine(String.Empty);
            return helpText;
        }

        /// <summary>
        /// Adds the URI schemes description to the help text.
        /// </summary>
        private void AddSchemesHelp(
            HelpText helpText,
            IEnumerable<string> schemes)
        {
            foreach (string scheme in schemes)
            {
                helpText.AddPostOptionsLine(String.Format(
                    "  {0}",
                    SchemeHelpStrings[scheme]));
            }
        }

        /// <summary>
        /// Adds the groupers description to the help text.
        /// </summary>
        private void AddGroupersHelp(
            HelpText helpText,
            IEnumerable<string> groupers)
        {
            foreach (string grouper in groupers)
            {
                helpText.AddPostOptionsLine(String.Format(
                    "  {0}\t{1}", 
                    grouper, 
                    GrouperHelpStrings[grouper]));
            }
        }
    }
}
