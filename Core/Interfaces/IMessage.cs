using System;
using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Represents a single message.
    /// </summary>
    internal interface IMessage
    {
        /// <summary>
        /// Gets the message class.
        /// </summary>
        MessageClass Class
        {
            get;
        }
    }
}
