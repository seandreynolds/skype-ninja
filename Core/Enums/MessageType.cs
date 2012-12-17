using System;

namespace eigenein.SkypeNinja.Core.Enums
{
    /// <summary>
    /// Message type (messages.type field type).
    /// </summary>
    internal enum MessageType
    {
        SetTopic = 2,
        ConferenceOpensUp = 4,
        VideosessionStarted = 30,
        VideosessionTerminated = 39,
        AuthorizationRequested = 50,
        AuthorizationAccepted = 51,
        Blocked = 53,
        SentEmoticon = 60,
        Said = 61,
        SentContact = 63,
        SentSms = 64,
        SentVoiceMessage = 65,
        SentFile = 68,
        Birthday = 110
    }
}
