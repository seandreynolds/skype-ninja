using System;
using System.Runtime.Serialization;
using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Exceptions
{
    /// <summary>
    /// The message being copied is skipped. This means that the message is
    /// expected to be not copied.
    /// </summary>
    [Serializable]
    public class MessageSkippedException : Exception
    {
        private readonly MessageSkipReason reason;

        public MessageSkippedException(string message, MessageSkipReason reason)
            : base(message)
        {
            this.reason = reason;
        }

        /// <summary>
        /// Gets the message skip reason.
        /// </summary>
        public MessageSkipReason Reason
        {
            get
            {
                return reason;
            }
        }

        public override void GetObjectData(
            SerializationInfo info,
            StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Reason", reason);
        }
    }
}
