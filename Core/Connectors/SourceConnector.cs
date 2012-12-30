using System;
using System.Collections.Generic;
using eigenein.SkypeNinja.Core.Connectors.Common.Collections;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors
{
    internal abstract class SourceConnector : Connector, ISourceConnector
    {
        protected SourceConnector(Uri uri) 
            : base(uri)
        {
            // Do nothing.
        }

        public abstract IMessageEnumerator QueryMessages(
            IEnumerable<FilterCollection> filters);
    }
}
