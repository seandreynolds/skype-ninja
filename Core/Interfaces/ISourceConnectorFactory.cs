using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    public interface ISourceConnectorFactory : IConnectorFactory
    {
        ISourceConnector CreateConnector(Uri uri);
    }
}
