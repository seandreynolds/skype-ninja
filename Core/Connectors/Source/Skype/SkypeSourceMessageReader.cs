using System;
using System.Collections.Generic;
using System.Data.SQLite;

using eigenein.SkypeNinja.Core.Common.Extensions;
using eigenein.SkypeNinja.Core.Connectors.Common;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Exceptions;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source.Skype
{
    internal static class SkypeSourceMessageReader
    {
        /// <summary>
        /// Reads the message from the data reader.
        /// </summary>
        private delegate IMessage ReadMessageFunc(IMessage message, SQLiteDataReader reader);

        private static readonly Dictionary<SkypeMessageType, ReadMessageFunc> FactoryByMessageType =
            new Dictionary<SkypeMessageType, ReadMessageFunc>()
                {
                    {SkypeMessageType.Said, ReadSaidMessage},
                };

        public static IMessage ReadMessage(SQLiteDataReader reader)
        {
            // Initialize the message.
            IMessage message = new Message();
            message.Properties.Add(
                PropertyType.MessageClass, 
                reader.GetEnum<MessageClass>("messageClass"));
            // Specific method used to read the message.
            ReadMessageFunc readMessage;
            // Try to read by the message type.
            SkypeMessageType messageType;
            if (reader.TryGetEnum("messageType", out messageType) &&
                FactoryByMessageType.TryGetValue(messageType, out readMessage))
            {
                message.Properties.Add(PropertyType.SkypeMessageType, messageType);
                return readMessage(message, reader);
            }
            // Could not read the message.
            throw new MessageSkippedException(MessageSkipReason.SkypeUnknownMessageType);
        }

        private static IMessage ReadSaidMessage(IMessage message, SQLiteDataReader reader)
        {
            // Add the message properties.
            message.Properties.Add(PropertyType.Body, reader.GetString("body"));
            message.Properties.Add(PropertyType.Author, reader.GetString("author"));
            message.Properties.Add(PropertyType.Timestamp, reader.GetDateTime("timestamp"));
            message.Properties.Add(PropertyType.FromDisplayName, reader.GetString("fromDisplayName"));
            // Add the message status (just a bit longer than other properties).
            SkypeChatMessageStatus skypeMessageStatus;
            if (!reader.TryGetEnum("chatMessageStatus", out skypeMessageStatus))
            {
                throw new MessageFailedException("Invalid message status.");
            }
            message.Properties.Add(PropertyType.SkypeMessageStatus, skypeMessageStatus);
            // Return the initialized message.
            return message;
        }
    }
}
