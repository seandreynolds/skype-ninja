using System;
using System.Collections.Generic;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    internal interface ISourceConnector : IConnector
    {
        IEnumerable<IMessage> QueryMessages(IFilter filter);
    }
}
