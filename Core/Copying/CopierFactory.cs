using System;
using eigenein.SkypeNinja.Core.Connectors.Common.Collections;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Copying
{
    public static class CopierFactory
    {
        public static ICopier CreateCopier(
            ISourceConnector sourceConnector,
            FilterCollection filters,
            ITargetConnector targetConnector)
        {
            return new UniversalCopier(
                sourceConnector.QueryMessages(filters), 
                targetConnector);
        }
    }
}
