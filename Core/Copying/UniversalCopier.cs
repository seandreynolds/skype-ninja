using System;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Copying
{
    internal class UniversalCopier : Copier
    {
        public UniversalCopier(
            IMessageEnumerator messageEnumerator,
            ITargetConnector targetConnector)
            : base(messageEnumerator, targetConnector)
        {
            // Do nothing.
        }

        /// <summary>
        /// Copies the next message.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the copier has successfully copied the next message;
        /// <c>false</c> if the underlying message enumerator has passed the end of the message collection.
        /// </returns>
        public override bool CopyNextMessage()
        {
            if (!MessageEnumerator.MoveNext())
            {
                return false;
            }

            TargetConnector.InsertMessage(MessageEnumerator.Current);
            return true;
        }
    }
}
