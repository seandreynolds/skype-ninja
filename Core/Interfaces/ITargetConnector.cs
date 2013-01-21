using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Represents a target connector.
    /// </summary>
    public interface ITargetConnector : IConnector
    {
        /// <summary>
        /// Inserts the message into the target.
        /// </summary>
        void InsertMessage(IMessage message);
    }
}
