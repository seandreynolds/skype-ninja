using System;
using System.Data.SQLite;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source.Skype
{
    [ConnectorFactory("Core.Connectors.Source.Skype.SkypeDbSourceConnectorFactory.Help")]
    internal class SkypeDbSourceConnectorFactory : ISourceConnectorFactory
    {
        #region Implementation of ISourceConnectorFactory

        public ISourceConnector CreateConnector(Uri uri)
        {
            if (String.IsNullOrWhiteSpace(uri.LocalPath))
            {
                throw new ArgumentException("Skype database path is expected.");
            }
            // Construct the connection string.
            string connectionString = String.Format(
                "Data Source={0};Read Only=True",
                uri.LocalPath);
            // Create and open the connection.
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            // Create and return the connector.
            return new SkypeSourceConnector(connection);
        }

        #endregion
    }
}
