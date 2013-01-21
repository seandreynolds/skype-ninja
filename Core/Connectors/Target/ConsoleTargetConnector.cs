using System;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

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

        /// <summary>
        /// Inserts the message into the target.
        /// </summary>
        public override void InsertMessage(IMessage message)
        {
            Console.WriteLine("Message {0}", message.MessageType);
            object body;
            if (message.Properties.TryGetItem(PropertyType.Body, out body))
            {
                Console.WriteLine("Body: {0}", body);
            }
            Console.WriteLine();
        }

        #endregion
    }
}
