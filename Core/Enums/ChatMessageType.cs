using System;

namespace eigenein.SkypeNinja.Core.Enums
{
    /// <summary>
    /// Chat message type (messages.chatmsg_type field type).
    /// </summary>
    internal enum ChatMessageType
    {
        AddedMembers = 1,
        CreatedChatWith = 2,
        Said = 3,
        Left = 4,
        SetTopic = 5,
        SentFile = 7,
        AddedContact = 18
    }
}
