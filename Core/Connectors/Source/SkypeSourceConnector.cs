using System;
using System.Collections.Generic;
using System.Data.SQLite;
using eigenein.SkypeNinja.Core.Connectors.Common;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source
{
    internal class SkypeSourceConnector : SourceConnector
    {
        private readonly SQLiteConnection connection;

        public SkypeSourceConnector(string path)
            : base(path)
        {
            string connectionString = String.Format(
                "Data Source={0};Read Only=True",
                path);
            connection = new SQLiteConnection(connectionString);
        }

        #region Overrides of Connector

        public override void Open()
        {
            connection.Open();
        }

        public override void Close()
        {
            connection.Close();
        }

        public override IMessageEnumerator QueryMessages(IEnumerable<Filter> filters)
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
