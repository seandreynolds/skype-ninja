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
        public MessageSkippedException(MessageSkipReason reason)
            : base(reason.ToString())
        {
            this.Reason = reason;
        }

        /// <summary>
        /// Gets the message skip reason.
        /// </summary>
        public MessageSkipReason Reason
        {
            get;
            private set;
        }

        public override void GetObjectData(
            SerializationInfo info,
            StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Reason", Reason);
        }
    }
}
