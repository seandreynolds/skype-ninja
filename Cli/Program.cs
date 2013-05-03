using System;
using System.Collections.Generic;
using NLog;
using eigenein.SkypeNinja.Cli.Common;
using eigenein.SkypeNinja.Core.Connectors;
using eigenein.SkypeNinja.Core.Connectors.Common.Collections;
using eigenein.SkypeNinja.Core.Copying;
using eigenein.SkypeNinja.Core.Exceptions;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Cli
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            Options options = new Options();
            CommandLine.Parser commandLineParser = new CommandLine.Parser(
                settings => settings.HelpWriter = Console.Error);
            if (commandLineParser.ParseArguments(args, options))
            {
                Main2(options);
            }
            else
            {
                Environment.Exit(ExitCode.CheckOptions);
            }
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
                    Environment.Exit(ExitCode.UnknownUriScheme);
                }
                if (!TryCreateConnector(
                    UniversalConnectorFactory.CreateTargetConnector,
                    targetUri,
                    out targetConnector))
                {
                    Logger.Fatal("Invalid target URI scheme.");
                    Environment.Exit(ExitCode.UnknownUriScheme);
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

                // TODO: filters.
                CopyMessages(sourceConnector, null, targetConnector);
            }
            catch (Exception ex)
            {
                Logger.FatalException("Copying has failed.", ex);
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

        private static void CopyMessages(
            ISourceConnector sourceConnector,
            FilterCollection filters,
            ITargetConnector targetConnector)
        {
            ICopier copier = CopierFactory.CreateCopier(
                sourceConnector, 
                filters,
                targetConnector);

            int messageCount = 0;
            int messageCopiedCount = 0;
            int messageSkippedCount = 0;

            while (true)
            {
                bool messageCollectionEndPassed = false;
                bool messageCopied = false;
                
                try
                {
                    messageCollectionEndPassed = !copier.CopyNextMessage();
                    messageCopied = true;
                }
                catch (MessageSkippedException)
                {
                    messageSkippedCount += 1;
                }
                catch (Exception ex)
                {
                    Logger.ErrorException("Could not migrate the message.", ex);
                }

                if (!messageCollectionEndPassed)
                {
                    messageCount += 1;
                    if (messageCopied)
                    {
                        messageCopiedCount += 1;
                    }
                    if (messageCount % 100 == 0)
                    {
                        Logger.Info("{0} of {1} messages copied.", messageCopiedCount, messageCount);
                    }
                }
                else
                {
                    break;
                }
            }

            Logger.Info("Copying has been finished.");
            Logger.Info("{0} messages copied.", messageCopiedCount);
            Logger.Info("{0} messages skipped.", messageSkippedCount);
            Logger.Info("{0} messages failed.", messageCount - messageCopiedCount - messageSkippedCount);
        }

        /// <summary>
        /// Tries to parse the URI.
        /// </summary>
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

        /// <summary>
        /// Tries to create a connector by the URI using the specified factory.
        /// </summary>
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

        /// <summary>
        /// Tries to open the connector.
        /// </summary>
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
