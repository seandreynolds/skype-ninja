using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    internal interface ITargetConnectorFactory
    {
        ITargetConnector CreateConnector(string path);
    }
}
