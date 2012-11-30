using System;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Target
{
    internal class ConsoleTargetConnectorFactory : ITargetConnectorFactory
    {
        #region Implementation of ITargetConnectorFactory

        public ITargetConnector CreateConnector(string path)
        {
            return new ConsoleTargetConnector(path);
        }

        #endregion
    }
}
