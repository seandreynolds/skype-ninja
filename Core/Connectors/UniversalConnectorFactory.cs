using System;
using System.Collections.Generic;

using eigenein.SkypeNinja.Core.Connectors.Source.Skype;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors
{
    /// <summary>
    /// Used to create a connector instance by the URI.
    /// </summary>
    public static class UniversalConnectorFactory
    {
        /// <summary>
        /// Maps the source URI scheme to the source connector factory.
        /// </summary>
        private static readonly Dictionary<string, ISourceConnectorFactory> SourceFactoryCache =
            new Dictionary<string, ISourceConnectorFactory>()
                {
                    {ConnectorUriScheme.SkypeDb, new SkypeDbSourceConnectorFactory()}
                };

        /// <summary>
        /// Maps the target URI scheme to the target connector factory.
        /// </summary>
        private static readonly Dictionary<string, ITargetConnectorFactory> TargetFactoryCache =
            new Dictionary<string, ITargetConnectorFactory>()
                {
                    // TODO.
                };

        public static IEnumerable<ISourceConnectorFactory> SourceConnectorFactories
        {
            get
            {
                return SourceFactoryCache.Values;
            }
        }

        public static IEnumerable<ITargetConnectorFactory> TargetConnectorFactories
        {
            get
            {
                return TargetFactoryCache.Values;
            }
        }

        /// <summary>
        /// Creates the source connector by the specified URI.
        /// </summary>
        public static ISourceConnector CreateSourceConnector(Uri uri)
        {
            ISourceConnectorFactory factory;
            if (!SourceFactoryCache.TryGetValue(uri.Scheme, out factory))
            {
                throw new KeyNotFoundException("Unknown source connector scheme.");
            }

            return factory.CreateConnector(uri);
        }

        /// <summary>
        /// Creates the target connector by the specified URI.
        /// </summary>
        public static ITargetConnector CreateTargetConnector(Uri uri)
        {
            ITargetConnectorFactory factory;
            if (!TargetFactoryCache.TryGetValue(uri.Scheme, out factory))
            {
                throw new KeyNotFoundException("Unknown target connector scheme.");
            }

            return factory.CreateConnector(uri);
        }
    }
}
