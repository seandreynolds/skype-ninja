using System;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors
{
    internal abstract class SourceConnector : Connector, ISourceConnector
    {
        public abstract IMessageEnumerator QueryMessages(
            FilterCollection filters);
    }
}
