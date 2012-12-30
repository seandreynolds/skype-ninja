using System;
using System.Collections.Generic;
using System.Data.SQLite;
using eigenein.SkypeNinja.Core.Connectors.Common.Collections;
using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Connectors.Common
{
    internal class SkypeSQLiteCommandFactory
    {
        private const string ReadMessagesQuery = @"
            select @chatMessageClassId as messageClassId,
                   author as author,
                   from_dispname as fromDisplayName,
                   timestamp as timestamp,
                   body_xml as body,
                   chatmsg_type as chatMessageType,
                   chatmsg_status as chatMessageStatus
        ";

        private readonly SQLiteConnection connection;

        public SkypeSQLiteCommandFactory(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        public SQLiteCommand CreateReadMessagesCommand(IEnumerable<FilterCollection> filters)
        {
            SQLiteCommand command = new SQLiteCommand(ReadMessagesQuery, connection);

            command.Parameters.Add(new SQLiteParameter(
                "@chatMessageClassId", 
                SkypeEntityClass.ChatMessage));

            return command;
        }
    }
}
