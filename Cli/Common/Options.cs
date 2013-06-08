using System;
using System.Collections.Generic;

using CommandLine;
using CommandLine.Text;

using eigenein.SkypeNinja.Core.Common;
using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Caches;
using eigenein.SkypeNinja.Core.Connectors;
using eigenein.SkypeNinja.Core.Interfaces;

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
            AddSchemesHelp(helpText, UniversalConnectorFactory.SourceConnectorFactories);
            helpText.AddPostOptionsLine("Available target schemes:");
            AddSchemesHelp(helpText, UniversalConnectorFactory.TargetConnectorFactories);
            // Done.
            helpText.AddPostOptionsLine(String.Empty);
            return helpText;
        }

        /// <summary>
        /// Adds the URI schemes description to the help text.
        /// </summary>
        private void AddSchemesHelp(
            HelpText helpText,
            IEnumerable<IConnectorFactory> factories)
        {
            foreach (IConnectorFactory factory in factories)
            {
                ConnectorFactoryAttribute attribute =
                    ClassAttributeCache<ConnectorFactoryAttribute>.GetAttribute(factory.GetType());
                helpText.AddPostOptionsLine(String.Format("  {0}", Translator.GetString(attribute.Help)));
            }
        }
    }
}
