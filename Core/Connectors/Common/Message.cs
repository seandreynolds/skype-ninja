using System;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common
{
    internal class Message : IMessage
    {
        private readonly MessageType messageType;

        private readonly PropertyCollection properties;

        public Message(MessageType messageType)
        {
            this.messageType = messageType;
            this.properties = new PropertyCollection();
        }

        public MessageType MessageType
        {
            get
            {
                return messageType;
            }
        }

        public PropertyCollection Properties
        {
            get
            {
                return properties;
            }
        }
    }
}
