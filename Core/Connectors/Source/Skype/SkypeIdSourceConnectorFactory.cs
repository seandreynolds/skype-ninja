using System;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source.Skype
{
    [ConnectorFactory("Core.Connectors.Source.Skype.SkypeIdSourceConnectorFactory.Help")]
    internal class SkypeIdSourceConnectorFactory : ISourceConnectorFactory
    {
        private static readonly char[] SkypeIdSeparators = new[] {'/', '\\'};

        private static bool ParseUri(Uri uri, out string userName, out string skypeId)
        {
            // Path optionally contains the Windows username.
            string[] parts = uri.LocalPath.Split(SkypeIdSeparators, 2);
            // skypeid://skypeid
            if (parts.Length == 1)
            {
                skypeId = parts[0];
                userName = Environment.UserName;
                return !String.IsNullOrWhiteSpace(skypeId);
            }
            // skypeid://username/skypeid
            userName = parts[0];
            skypeId = parts[1];
            return !String.IsNullOrWhiteSpace(userName) && !String.IsNullOrWhiteSpace(skypeId);
        }

        private static bool GetSkypeDatabaseLocator(out ISkypeDatabaseLocator locator)
        {
            throw new NotImplementedException();
        }

        public ISourceConnector CreateConnector(Uri uri)
        {
            if (String.IsNullOrWhiteSpace(uri.LocalPath))
            {
                throw new ArgumentException("Skype ID is required.");
            }
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
            string datapasePath = locator.FindDatabase(userName, skypeId);
            // Initialize the connector from the obtained path.
            return SkypeSourceConnector.FromFile(datapasePath);
        }
    }
}
