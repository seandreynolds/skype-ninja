using System;

using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Copying
{
    public class Copier
    {
        protected readonly IMessageEnumerator MessageEnumerator;

        protected readonly ITargetConnector TargetConnector;

        public Copier(
            IMessageEnumerator messageEnumerator,
            ITargetConnector targetConnector)
        {
            this.MessageEnumerator = messageEnumerator;
            this.TargetConnector = targetConnector;
        }

        /// <summary>
        /// Copies the next message.
        /// </summary>
        public bool CopyNextMessage()
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
