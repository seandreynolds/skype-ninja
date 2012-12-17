using System;

namespace eigenein.SkypeNinja.Core.Enums
{
    /// <summary>
    /// Chat message status (messages.chatmsg_status field type).
    /// </summary>
    internal enum ChatMessageStatus
    {
        Sending = 1,
        Sent = 2,
        Received = 3,
        Read = 4
    }
}
