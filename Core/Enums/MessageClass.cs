using System;

namespace eigenein.SkypeNinja.Core.Enums
{
    /// <summary>
    /// Class of the message (chat message, call, ...).
    /// </summary>
    internal enum MessageClass
    {
        /// <summary>
        /// Unknown message class (should not be used explicitly).
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Simple chat message.
        /// </summary>
        ChatMessage = 1,

        /// <summary>
        /// Skype call.
        /// </summary>
        Call = 2,

        /// <summary>
        /// SMS.
        /// </summary>
        Sms = 3,

        /// <summary>
        /// File transfer.
        /// </summary>
        Transfer = 4,

        /// <summary>
        /// Video message.
        /// </summary>
        VideoMessage = 5,

        /// <summary>
        /// Voice mail.
        /// </summary>
        VoiceMail = 6
    }
}
