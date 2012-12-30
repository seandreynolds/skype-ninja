using System;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors
{
    internal abstract class TargetConnector : Connector, ITargetConnector
    {
        protected TargetConnector(Uri uri) 
            : base(uri)
        {
            // Do nothing.
        }
    }
}
