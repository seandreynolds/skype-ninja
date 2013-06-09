using System;

using eigenein.SkypeNinja.Core.Connectors.Common.Skype;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source.Skype
{
    internal class SkypeIdSourceConnectorFactory : ISourceConnectorFactory
    {
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

        /// <summary>
        /// Creates the connector by the specified URI.
        /// </summary>
        public ISourceConnector CreateConnector(Uri uri)
        {
            // Parse the URI.
            string skypeId = uri.Host;
            if (String.IsNullOrWhiteSpace(skypeId))
            {
                throw new ArgumentException("Skype ID is expected.");
            }
            // Get the database locator.
            ISkypeDatabaseLocator locator;
            if (!GetSkypeDatabaseLocator(out locator))
            {
                throw new ArgumentException(String.Format(
                    "Unsupported environment: {0}.", 
                    Environment.OSVersion));
            }
            // Get the database location.
            string databasePath;
            if (!locator.FindDatabase(skypeId, out databasePath))
            {
                throw new ArgumentException(String.Format("Database is not found at: \"{0}\".", databasePath));
            }
            // Initialize the connector with the obtained path.
            return SkypeSourceConnector.FromFile(databasePath);
        }
    }
}
