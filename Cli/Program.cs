using System;
using System.Collections.Generic;
using CommandLine;
using NLog;
using eigenein.SkypeNinja.Core.Connectors;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Cli
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            Options options = new Options();
            CommandLineParser parser = new CommandLineParser(
                new CommandLineParserSettings(Console.Error));
            if (!parser.ParseArguments(args, options))
            {
                Logger.Fatal("Invalid options.");
                Environment.Exit(ExitCode.InvalidOptions);
            }

            Main2(options);
        }

        private static void Main2(Options options)
        {
            Uri sourceUri;

            if (!TryParseUri(options.SourceUriString, out sourceUri))
            {
                Logger.Fatal("Could not parse source URI.");
                Environment.Exit(ExitCode.InvalidUri);
            }

            Uri targetUri;

            if (!TryParseUri(options.TargetUriString, out targetUri))
            {
                Logger.Fatal("Could not parse target URI.");
                Environment.Exit(ExitCode.InvalidUri);
            }

            ISourceConnector sourceConnector = null;
            ITargetConnector targetConnector = null;

            try
            {
                if (!TryCreateConnector(
                    UniversalConnectorFactory.CreateSourceConnector,
                    sourceUri,
                    out sourceConnector))
                {
                    Logger.Fatal("Invalid source URI scheme.");
                    Environment.Exit(ExitCode.UnknownScheme);
                }
                if (!TryCreateConnector(
                    UniversalConnectorFactory.CreateTargetConnector,
                    targetUri,
                    out targetConnector))
                {
                    Logger.Fatal("Invalid target URI scheme.");
                    Environment.Exit(ExitCode.UnknownScheme);
                }
                if (!TryOpenConnector(sourceConnector))
                {
                    Logger.Fatal("Could not open source connector.");
                    Environment.Exit(ExitCode.ConnectorOpenFailed);
                }
                if (!TryOpenConnector(targetConnector))
                {
                    Logger.Fatal("Could not open target connector.");
                    Environment.Exit(ExitCode.ConnectorOpenFailed);
                }
            }
            finally
            {
                if (sourceConnector != null)
                {
                    sourceConnector.Dispose();
                }
                if (targetConnector != null)
                {
                    targetConnector.Dispose();
                }
            }
        }

        private static bool TryParseUri(string uriString, out Uri uri)
        {
            try
            {
                uri = new Uri(uriString);
                return true;
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Error parsing the URI.", ex);
                uri = null;
                return false;
            }
        }

        private static bool TryCreateConnector<TConnector>(
            Func<Uri, TConnector> factory,
            Uri uri, 
            out TConnector connector)
            where TConnector : IConnector
        {
            try
            {
                Logger.Info("Creating the connector {0} ...", uri);
                connector = factory(uri);
                return true;
            }
            catch (KeyNotFoundException)
            {
                Logger.Error("Unknown scheme: {0}.", uri.Scheme);
                connector = default(TConnector);
                return false;
            }
        }

        private static bool TryOpenConnector(IConnector connector)
        {
            try
            {
                Logger.Info("Opening the connector {0} ...", connector.Uri);
                connector.Open();
                return true;
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Error opening the connector.", ex);
                return false;
            }
        }
    }
}
