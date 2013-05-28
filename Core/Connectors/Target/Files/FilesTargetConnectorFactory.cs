using System;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Target.Files
{
    [ConnectorFactory("Core.Connectors.Target.Files.FilesTargetConnectorFactory.Help")]
    internal class FilesTargetConnectorFactory : ITargetConnectorFactory
    {
        public ITargetConnector CreateConnector(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
