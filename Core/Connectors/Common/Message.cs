using System;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common
{
    internal class Message : IMessage
    {
        private readonly PropertyCollection properties;

        public Message()
        {
            this.properties = new PropertyCollection();
        }

        public PropertyCollection Properties
        {
            get
            {
                return properties;
            }
        }
    }
}
