using System;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Copying
{
    internal abstract class Copier : ICopier
    {
        protected readonly IMessageEnumerator MessageEnumerator;

        protected readonly ITargetConnector TargetConnector;

        protected Copier(
            IMessageEnumerator messageEnumerator,
            ITargetConnector targetConnector)
        {
            this.MessageEnumerator = messageEnumerator;
            this.TargetConnector = targetConnector;
        }

        /// <summary>
        /// Copies the next message.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the copier has successfully copied the next message;
        /// <c>false</c> if the underlying message enumerator has passed the end of the message collection.
        /// </returns>
        public abstract bool CopyNextMessage();
    }
}
