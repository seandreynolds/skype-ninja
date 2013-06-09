using System;

using eigenein.SkypeNinja.Core.Common.Collections;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Represents a single message.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the message properties.
        /// </summary>
        PropertyCollection Properties
        {
            get;
        }
    }
}
