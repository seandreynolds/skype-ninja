using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    internal interface IConnector : IDisposable
    {
        string Path
        {
            get;
        }

        void Open();

        void Close();
    }
}
