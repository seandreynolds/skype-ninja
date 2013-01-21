using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Represents a message copier.
    /// </summary>
    public interface ICopier
    {
        /// <summary>
        /// Copies the next message.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the copier has successfully copied the next message;
        /// <c>false</c> if the underlying message enumerator has passed the end of the message collection.
        /// </returns>
        bool CopyNextMessage();
    }
}
