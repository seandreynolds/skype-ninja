using System;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Extensions;
using eigenein.SkypeNinja.Core.Common.Helpers;
using eigenein.SkypeNinja.Core.Connectors.Common.Skype;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source.Skype
{
    [ConnectorFactory("Core.Connectors.Source.Skype.SkypeIdSourceConnectorFactory.Help")]
    internal class SkypeIdSourceConnectorFactory : ISourceConnectorFactory
    {
        private static bool ParseUri(Uri uri, out string userName, out string skypeId)
        {
            // Parse the query parameters.
            QueryParameters parameters = uri.GetQueryParameters();
            if (!parameters.TryGetValue("user", out userName))
            {
                // Use current user by default.
                userName = Environment.UserName;
            }
            // Skype ID is the URI "host" part.
            skypeId = uri.Host;
            // Validate the user name.
            return !String.IsNullOrWhiteSpace(userName);
        }

        /// <summary>
        /// Gets the database locator for current environment.
        /// </summary>
        private static bool GetSkypeDatabaseLocator(out ISkypeDatabaseLocator locator)
        {
            // Windows.
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                locator = new SkypeDatabaseLocator();
                return true;
            }
            // The OS is not supported.
            locator = null;
            return false;
        }

        public ISourceConnector CreateConnector(Uri uri)
        {
            // Parse the URI.
            string userName;
            string skypeId;
            if (!ParseUri(uri, out userName, out skypeId))
            {
                throw new ArgumentException(String.Format("Invalid URI: \"{0}\".", uri));
            }
            // Get the database locator.
            ISkypeDatabaseLocator locator;
            if (!GetSkypeDatabaseLocator(out locator))
            {
                throw new ArgumentException(String.Format("Unsupported environment: {0}.", Environment.OSVersion));
            }
            // Get the database location.
            string databasePath;
            if (!locator.FindDatabase(userName, skypeId, out databasePath))
            {
                throw new ArgumentException(String.Format("Database is not found at: \"{0}\".", databasePath));
            }
            // Initialize the connector from the obtained path.
            return SkypeSourceConnector.FromFile(databasePath);
        }
    }
}
