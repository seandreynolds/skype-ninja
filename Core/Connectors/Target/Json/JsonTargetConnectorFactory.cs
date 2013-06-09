using System;
using System.IO;
using System.Text;

using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Target.Json
{
    internal class JsonTargetConnectorFactory : ITargetConnectorFactory
    {
        public ITargetConnector CreateConnector(Uri uri)
        {
            return new JsonTargetConnector(
                String.IsNullOrWhiteSpace(uri.LocalPath)
                    ? Console.Out
                    : new StreamWriter(uri.LocalPath, false, Encoding.UTF8));
        }
    }
}
