using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    public interface ITargetConnectorFactory : IConnectorFactory
    {
        ITargetConnector CreateConnector(Uri uri);
    }
}
