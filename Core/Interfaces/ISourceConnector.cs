using System;

using eigenein.SkypeNinja.Core.Common.Collections;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    public interface ISourceConnector : IConnector
    {
        IMessageEnumerator QueryMessages(FilterCollection filters);
    }
}
