using System;
using System.Collections.Generic;
using eigenein.SkypeNinja.Core.Connectors.Common.Collections;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    public interface ISourceConnector : IConnector
    {
        IMessageEnumerator QueryMessages(IEnumerable<FilterCollection> filters);
    }
}
