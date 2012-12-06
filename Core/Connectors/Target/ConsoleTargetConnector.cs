using System;
using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Connectors.Target
{
    internal class ConsoleTargetConnector : TargetConnector
    {
        public ConsoleTargetConnector(string path) 
            : base(path)
        {
            // Do nothing.
        }

        #region Overrides of Connector

        public override void Open()
        {
            Console.WriteLine(
                "Opened {0}://{1}", 
                ConnectorUriScheme.Console,
                Path);
        }

        public override void Close()
        {
            Console.WriteLine(
                "Closed {0}://{1}",
                ConnectorUriScheme.Console,
                Path);
            Console.Out.Flush();
        }

        #endregion
    }
}
