using System;
using eigenein.SkypeNinja.Core.Connectors.Common.Collections;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common.Messages
{
    internal class Message : IMessage
    {
        private readonly MessageType messageType;

        private readonly PropertyCollection properties;

        public Message(MessageType messageType, PropertyCollection properties)
        {
            this.messageType = messageType;
            this.properties = properties;
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
