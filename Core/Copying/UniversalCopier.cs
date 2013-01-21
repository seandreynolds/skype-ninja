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

        protected override IMessage ConvertMessage(IMessage message)
        {
            return message;
        }
    }
}
