using System;
using System.Data.SQLite;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common
{
    internal static class SkypeSourceMessageFactory
    {
        public static IMessage ReadMessage(SQLiteDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
