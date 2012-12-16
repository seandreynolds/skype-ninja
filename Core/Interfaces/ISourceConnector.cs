using System;
using System.Collections.Generic;
using eigenein.SkypeNinja.Core.Connectors.Common;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    internal interface ISourceConnector : IConnector
    {
        IMessageEnumerator QueryMessages(IEnumerable<Filter> filters);
    }
}
