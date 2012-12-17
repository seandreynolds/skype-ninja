using System;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common.Messages
{
    internal abstract class Message : IMessage
    {
        private readonly DateTime timeStamp;

        protected Message(DateTime timeStamp)
        {
            this.timeStamp = timeStamp;
        }

        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }
        }
    }
}
