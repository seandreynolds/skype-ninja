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
        public bool CopyNextMessage()
        {
            if (!MessageEnumerator.MoveNext())
            {
                return false;
            }

            IMessage convertedMessage = ConvertMessage(MessageEnumerator.Current);
            TargetConnector.InsertMessage(convertedMessage);
            return true;
        }

        /// <summary>
        /// Converts the message.
        /// </summary>
        protected abstract IMessage ConvertMessage(IMessage message);
    }
}
