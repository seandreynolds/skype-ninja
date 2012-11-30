using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    internal interface ISourceConnectorFactory : IConnectorFactory
    {
        ISourceConnector CreateConnector(string path);
    }
}
