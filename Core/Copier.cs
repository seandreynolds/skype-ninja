using System;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core
{
    public class Copier
    {
        private readonly IMessageEnumerator messageEnumerator;

        private readonly GrouperCollection groupers;

        private readonly ITargetConnector targetConnector;

        public Copier(
            IMessageEnumerator messageEnumerator,
            GrouperCollection groupers,
            ITargetConnector targetConnector)
        {
            this.messageEnumerator = messageEnumerator;
            this.groupers = groupers;
            this.targetConnector = targetConnector;
        }

        /// <summary>
        /// Copies the next message.
        /// </summary>
        public bool CopyNextMessage()
        {
            if (!messageEnumerator.MoveNext())
            {
                return false;
            }

            targetConnector.InsertMessage(messageEnumerator.Current);
            return true;
        }
    }
}
