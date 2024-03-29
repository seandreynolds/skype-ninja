﻿using System;
using System.Collections.Generic;
using NLog;
using eigenein.SkypeNinja.Cli.Common;
using eigenein.SkypeNinja.Core;
using eigenein.SkypeNinja.Core.Common;
using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Common.Extensions;
using eigenein.SkypeNinja.Core.Connectors;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Exceptions;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Cli
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Entry point wrapper. Parses the arguments.
        /// </summary>
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

        /// <summary>
        /// Actual entry point.
        /// </summary>
        private static void Main2(Options options)
        {
            Uri sourceUri;

            if (!UriExtensions.TryParse(options.SourceUriString, out sourceUri))
            {
                Logger.Fatal("Could not parse source URI.");
                Environment.Exit(ExitCode.InvalidUri);
            }

            Uri targetUri;

            if (!UriExtensions.TryParse(options.TargetUriString, out targetUri))
            {
                Logger.Fatal("Could not parse target URI.");
                Environment.Exit(ExitCode.InvalidUri);
            }

            ISourceConnector sourceConnector = null;
            ITargetConnector targetConnector = null;
            Statistics statistics = null;

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

                // Initialize filtering and grouping rules.
                FilterCollection filters = FilterCollection.FromString(options.Filters);
                GrouperCollection groupers = GrouperCollection.FromString(options.Groups);
                // Copy messages.
                statistics = CopyMessages(sourceConnector, filters, groupers, targetConnector);
            }
            catch (Exception ex)
            {
                Logger.Fatal("Copying has failed: {0}", ex.Message);
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
            // Finished.
            Logger.Info("Copying has been finished.");
            // Print the statistics.
            if (statistics != null)
            {
                foreach (KeyValuePair<StatisticsType, int> statisticsItem in statistics)
                {
                    Logger.Info("{0}: {1}.", statisticsItem.Key, statisticsItem.Value);
                }
            }
        }

        /// <summary>
        /// Performs copying of messages.
        /// </summary>
        private static Statistics CopyMessages(
            ISourceConnector sourceConnector,
            FilterCollection filters,
            GrouperCollection groupers,
            ITargetConnector targetConnector)
        {
            Copier copier = new Copier(
                sourceConnector.QueryMessages(filters),
                groupers,
                targetConnector);

            Statistics statistics = new Statistics();

            while (true)
            {
                try
                {
                    if (!copier.CopyNextMessage())
                    {
                        break;
                    }
                    // Update the statistics.
                    statistics[StatisticsType.Total] += 1;
                    statistics[StatisticsType.Copied] += 1;
                }
                catch (MessageSkippedException ex)
                {
                    Logger.Info("The message is skipped: {0}", ex.Message);
                    statistics[StatisticsType.Total] += 1;
                    statistics[StatisticsType.Skipped] += 1;
                }
                catch (Exception ex)
                {
                    Logger.Error("Failed to copy the message: {0}", ex.Message);
                }
            }

            return statistics;
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
            catch (Exception ex)
            {
                Logger.Error("Could not create the connector: {0}", ex.Message);
                connector = default(TConnector);
                return false;
            }
        }
    }
}
