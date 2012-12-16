using System;
using System.Collections.Generic;
using eigenein.SkypeNinja.Core.Connectors.Common;
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

        public abstract IMessageEnumerator QueryMessages(IEnumerable<Filter> filters);
    }
}
