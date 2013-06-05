using System;
using System.Data.SQLite;

using eigenein.SkypeNinja.Core.Connectors.Common.Collections;
using eigenein.SkypeNinja.Core.Connectors.Common.Skype;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source.Skype
{
    internal class SkypeSourceConnector : SourceConnector
    {
        private readonly SQLiteConnection connection;

        public SkypeSourceConnector(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        #region Overrides of Connector

        public override void Close()
        {
            connection.Close();
        }

        public override IMessageEnumerator QueryMessages(FilterCollection filters)
        {
            SkypeSQLiteCommandFactory commandFactory = new SkypeSQLiteCommandFactory(
                connection);

            using (SQLiteCommand command = commandFactory.CreateReadMessagesCommand(filters))
            {
                SQLiteDataReader reader = command.ExecuteReader();
                return new SkypeSourceMessageEnumerator(reader);
            }
        }

        #endregion
    }
}
