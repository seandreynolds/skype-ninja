﻿using System;
using System.Data.SQLite;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Connectors.Common.Skype;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source.Skype
{
    internal class SkypeSourceConnector : SourceConnector
    {
        private readonly SQLiteConnection connection;

        /// <summary>
        /// Initializes an instance of <see cref="SkypeSourceConnector"/> from the
        /// specified database path.
        /// </summary>
        public static SkypeSourceConnector FromFile(string path)
        {
            // Construct the connection string.
            string connectionString = String.Format("Data Source={0};Read Only=True", path);
            // Create and open the connection.
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            // Create and return the connector.
            return new SkypeSourceConnector(connection);
        }

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
