using System;
using System.Collections.Generic;
using eigenein.SkypeNinja.Core.Connectors.Source;
using eigenein.SkypeNinja.Core.Connectors.Target;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors
{
    internal static class UniversalConnectorFactory
    {
        private static readonly Dictionary<string, ISourceConnectorFactory> SourceFactoryCache =
            new Dictionary<string, ISourceConnectorFactory>()
                {
                    {ConnectorUriScheme.Skype, new SkypeSourceConnectorFactory()}
                };

        private static readonly Dictionary<string, ITargetConnectorFactory> TargetFactoryCache =
            new Dictionary<string, ITargetConnectorFactory>()
                {
                    {ConnectorUriScheme.Console, new ConsoleTargetConnectorFactory()}
                };

        public static ISourceConnector CreateSourceConnector(Uri uri)
        {
            ISourceConnectorFactory factory;
            if (!SourceFactoryCache.TryGetValue(uri.Scheme, out factory))
            {
                throw new KeyNotFoundException("Unknown source connector scheme.");
            }

            return factory.CreateConnector(uri.PathAndQuery);
        }

        public static ITargetConnector CreateTargetConnector(Uri uri)
        {
            ITargetConnectorFactory factory;
            if (!TargetFactoryCache.TryGetValue(uri.Scheme, out factory))
            {
                throw new KeyNotFoundException("Unknown target connector scheme.");
            }

            return factory.CreateConnector(uri.PathAndQuery);
        }
    }
}
