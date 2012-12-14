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

        public override IEnumerable<IMessage> QueryMessages(IFilter filter)
        {
            // TODO: Make static Build that creates a builder and builds.
            IQueryBuilder<string> builder = new SkypeQueryBuilder();
            string query = builder.Build(filter.ToPartialQuery(builder));
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return ReadMessage(reader);
                        }
                    }
                }
            }
        }

        #endregion

        private static IMessage ReadMessage(SQLiteDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
