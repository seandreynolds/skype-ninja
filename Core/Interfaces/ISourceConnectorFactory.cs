using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Used to instantiate a source connector.
    /// </summary>
    public interface ISourceConnectorFactory : IConnectorFactory
    {
        /// <summary>
        /// Creates the connector by the specified URI.
        /// </summary>
        ISourceConnector CreateConnector(Uri uri);
    }
}
