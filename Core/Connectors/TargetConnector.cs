using System;

using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors
{
    internal abstract class TargetConnector : Connector, ITargetConnector
    {
        /// <summary>
        /// Inserts the message into the target.
        /// </summary>
        public abstract void InsertMessage(IMessage message);
    }
}
