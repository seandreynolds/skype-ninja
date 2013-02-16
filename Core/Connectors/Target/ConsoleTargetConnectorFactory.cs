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
                throw new ArgumentException("The connector has no parameters.");
            }
            return new ConsoleTargetConnector(uri);
        }

        #endregion
    }
}
