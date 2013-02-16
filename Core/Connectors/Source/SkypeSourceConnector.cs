using System;
using System.Data.SQLite;
using eigenein.SkypeNinja.Core.Connectors.Common;
using eigenein.SkypeNinja.Core.Connectors.Common.Collections;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source
{
    internal class SkypeSourceConnector : SourceConnector
    {
        private readonly SQLiteConnection connection;

        public SkypeSourceConnector(Uri uri)
            : base(uri)
        {
            string connectionString = String.Format(
                "Data Source={0};Read Only=True",
                uri.LocalPath);
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
