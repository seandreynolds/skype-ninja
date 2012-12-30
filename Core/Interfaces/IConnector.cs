using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    public interface IConnector : IDisposable
    {
        Uri Uri
        {
            get;
        }

        void Open();

        void Close();
    }
}
