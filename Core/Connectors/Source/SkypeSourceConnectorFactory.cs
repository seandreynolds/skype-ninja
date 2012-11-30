using System;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source
{
    internal class SkypeSourceConnectorFactory : ISourceConnectorFactory
    {
        #region Implementation of ISourceConnectorFactory

        public ISourceConnector CreateConnector(string path)
        {
            return new SkypeSourceConnector(path);
        }

        #endregion
    }
}
