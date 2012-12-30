using System;

namespace eigenein.SkypeNinja.Core.Connectors.Target
{
    internal class ConsoleTargetConnector : TargetConnector
    {
        public ConsoleTargetConnector(Uri uri) 
            : base(uri)
        {
            // Do nothing.
        }

        #region Overrides of Connector

        public override void Open()
        {
            Console.WriteLine(
                "Opened {0}", 
                Uri);
        }

        public override void Close()
        {
            Console.WriteLine(
                "Closed {0}",
                Uri);
            Console.Out.Flush();
        }

        #endregion
    }
}
