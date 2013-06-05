using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    public interface IConnector : IDisposable
    {
        void Close();
    }
}
