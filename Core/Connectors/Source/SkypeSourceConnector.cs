using System;

namespace eigenein.SkypeNinja.Core.Connectors.Source
{
    internal class SkypeSourceConnector : SourceConnector
    {
        public SkypeSourceConnector(string path)
            : base(path)
        {
            // Do nothing.
        }

        #region Overrides of Connector

        public override void Open()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
