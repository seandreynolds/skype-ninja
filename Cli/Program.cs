using System;
using System.Collections.Generic;
using CommandLine;
using eigenein.SkypeNinja.Core.Connectors;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Cli
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Options options = new Options();
            CommandLineParser parser = new CommandLineParser(
                new CommandLineParserSettings(Console.Error));
            if (!parser.ParseArguments(args, options))
            {
                Environment.Exit(ExitCode.InvalidOptions);
            }

            Main2(options);
        }

        private static void Main2(Options options)
        {
            Uri sourceUri;

            if (!TryParseUri(options.SourceUriString, out sourceUri))
            {
                Environment.Exit(ExitCode.InvalidUri);
            }

            Uri targetUri;

            if (!TryParseUri(options.TargetUriString, out targetUri))
            {
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
                    Environment.Exit(ExitCode.UnknownScheme);
                }
                if (!TryCreateConnector(
                    UniversalConnectorFactory.CreateTargetConnector,
                    targetUri,
                    out targetConnector))
                {
                    Environment.Exit(ExitCode.UnknownScheme);
                }
                if (!TryOpenConnector(sourceConnector))
                {
                    Environment.Exit(ExitCode.ConnectorOpenFailed);
                }
                if (!TryOpenConnector(targetConnector))
                {
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
            catch
            {
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
                connector = factory(uri);
                return true;
            }
            catch (KeyNotFoundException)
            {
                connector = default(TConnector);
                return false;
            }
        }

        private static bool TryOpenConnector(IConnector connector)
        {
            try
            {
                connector.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
