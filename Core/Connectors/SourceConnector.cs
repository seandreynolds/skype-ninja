using System;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors
{
    internal abstract class SourceConnector : Connector, ISourceConnector
    {
        protected SourceConnector(string path) 
            : base(path)
        {
            // Do nothing.
        }
    }
}
