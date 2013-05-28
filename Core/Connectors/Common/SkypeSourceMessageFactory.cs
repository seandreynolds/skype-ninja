using System;
using System.Collections.Generic;
using System.Data.SQLite;

using eigenein.SkypeNinja.Core.Common.Extensions;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Exceptions;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common
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

            throw new MessageSkippedException(
                "Unknown message type.",
                MessageSkipReason.SkypeUnknownMessageType);
        }

        private static IMessage CreateSaidMessage(SQLiteDataReader reader)
        {
            IMessage message = new Message(MessageType.Said);

            message.Properties.Add(PropertyType.Body, reader.GetString("body"));

            return message;
        }
    }
}
