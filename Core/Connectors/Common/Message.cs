using System;
using System.Text;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Enums;
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
        /// Gets the message properties.
        /// </summary>
        public PropertyCollection Properties
        {
            get;
            private set;
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
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            // Message class.
            object classProperty;
            if (Properties.TryGet(PropertyType.Class, out classProperty))
            {
                stringBuilder.AppendFormat("{0}", classProperty);
            }
            // Message ID.
            object idProperty;
            if (Properties.TryGet(PropertyType.Id, out idProperty))
            {
                stringBuilder.AppendFormat("#{0}", idProperty);
            }
            // Return the constructed string.
            return stringBuilder.ToString();
        }
    }
}
