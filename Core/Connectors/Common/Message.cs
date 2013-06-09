using System;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common
{
    internal class Message : IMessage
    {
        /// <summary>
        /// Creates a copy of the specified message.
        /// </summary>
        public static IMessage Copy(IMessage message)
        {
            return new Message(new PropertyCollection(message.Properties));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Message"/>.
        /// </summary>
        public Message()
        {
            this.Properties = new PropertyCollection();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Message"/> with
        /// thi given properties.
        /// </summary>
        public Message(PropertyCollection properties)
        {
            this.Properties = properties;
        }

        /// <summary>
        /// Gets the message properties.
        /// </summary>
        public PropertyCollection Properties
        {
            get;
            private set;
        }
    }
}
