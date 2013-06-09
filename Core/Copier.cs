using System;

using eigenein.SkypeNinja.Core.Common.Collections;
using eigenein.SkypeNinja.Core.Connectors.Common;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Exceptions;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core
{
    public class Copier
    {
        private readonly IMessageEnumerator messageEnumerator;

        private readonly GrouperCollection groupers;

        private readonly ITargetConnector targetConnector;

        public Copier(
            IMessageEnumerator messageEnumerator,
            GrouperCollection groupers,
            ITargetConnector targetConnector)
        {
            this.messageEnumerator = messageEnumerator;
            this.groupers = groupers;
            this.targetConnector = targetConnector;
        }

        /// <summary>
        /// Copies the next message.
        /// </summary>
        public bool CopyNextMessage()
        {
            // Move to the next message.
            if (!messageEnumerator.MoveNext())
            {
                return false;
            }
            // Create a copy of the source message.
            IMessage targetMessage = Message.Copy(messageEnumerator.Current);
            // Apply grouping.
            ApplyGrouping(targetMessage);
            // Insert the message.
            targetConnector.InsertMessage(targetMessage);
            // The message is copied.
            return true;
        }

        private void ApplyGrouping(IMessage message)
        {
            // Remove the group property if any.
            object oldGroup;
            if (message.Properties.TryGet(PropertyType.Group, out oldGroup))
            {
                message.Properties.Remove(PropertyType.Group);
            }
            // Add the new property.
            MessageGroup group = new MessageGroup();
            foreach (IGrouper grouper in groupers)
            {
                string part = grouper.GetGroupPart(message);
                group.AddPart(part);
            }
            message.Properties.Add(PropertyType.Group, group);
        }
    }
}
