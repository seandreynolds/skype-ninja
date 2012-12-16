using System;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common.Messages
{
    internal class ChatMessage : IMessage
    {
        public MessageClass Class
        {
            get
            {
                return MessageClass.ChatMessage;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
