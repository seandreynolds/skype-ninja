using System;
using System.Data.SQLite;

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

        #endregion
    }
}
