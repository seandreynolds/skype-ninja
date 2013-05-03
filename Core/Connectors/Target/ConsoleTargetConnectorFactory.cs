using System;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Target
{
    internal class ConsoleTargetConnectorFactory : ITargetConnectorFactory
    {
        #region Implementation of ITargetConnectorFactory

        public ITargetConnector CreateConnector(Uri uri)
        {
            if (!String.IsNullOrEmpty(uri.LocalPath))
            {
                throw new ArgumentException(String.Format(
                    "The connector has no parameters but found: {0}.", uri.LocalPath));
            }
            return new ConsoleTargetConnector(uri);
        }

        #endregion
    }
}
