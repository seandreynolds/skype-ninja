using System;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Represents a single message.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Get the storage-independent message type.
        /// </summary>
        MessageType MessageType
        {
            get;
        }

        /// <summary>
        /// Gets the message properties.
        /// </summary>
        PropertyCollection Properties
        {
            get;
        }
    }
}
