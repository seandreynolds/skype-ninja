using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Represents a single message.
    /// </summary>
    internal interface IMessage
    {
        /// <summary>
        /// Gets the message timestamp.
        /// </summary>
        DateTime TimeStamp
        {
            get;
        }
    }
}
