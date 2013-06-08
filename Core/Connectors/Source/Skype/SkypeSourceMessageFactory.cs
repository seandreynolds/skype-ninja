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
    internal static class SkypeSourceMessageFactory
    {
        /// <summary>
        /// Reads the message from the data reader.
        /// </summary>
        private delegate IMessage ReadMessageFunc(SQLiteDataReader reader);

        private static readonly Dictionary<SkypeMessageType, ReadMessageFunc> FactoryByMessageType =
            new Dictionary<SkypeMessageType, ReadMessageFunc>()
                {
                    {SkypeMessageType.Said, CreateSaidMessage},
                };

        private static readonly Dictionary<SkypeChatMessageType, ReadMessageFunc> FactoryByChatMessageType =
            new Dictionary<SkypeChatMessageType, ReadMessageFunc>()
                {
                    // TODO.
                };

        public static IMessage CreateMessage(SQLiteDataReader reader)
        {
            ReadMessageFunc readMessage;

            SkypeMessageType messageType;
            if (reader.TryGetEnum("messageType", out messageType) &&
                FactoryByMessageType.TryGetValue(messageType, out readMessage))
            {
                return readMessage(reader);
            }

            SkypeChatMessageType chatMessageType;
            if (reader.TryGetEnum("chatMessageType", out chatMessageType) &&
                FactoryByChatMessageType.TryGetValue(chatMessageType, out readMessage))
            {
                return readMessage(reader);
            }

            throw new MessageSkippedException(MessageSkipReason.SkypeUnknownMessageType);
        }

        private static IMessage CreateSaidMessage(SQLiteDataReader reader)
        {
            // Initialize the message.
            IMessage message = new Message(MessageType.Said);
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
