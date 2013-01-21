using System;

namespace eigenein.SkypeNinja.Core.Exceptions
{
    /// <summary>
    /// The message being copied is skipped. This means that the message is
    /// expected to be not copied.
    /// </summary>
    [Serializable]
    public class MessageSkippedException : Exception
    {
        public MessageSkippedException(string message)
            : base(message)
        {
            // Do nothing.
            // TODO: introduce skip reason and grouping by the reason.
        }
    }
}
