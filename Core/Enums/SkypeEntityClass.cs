using System;

namespace eigenein.SkypeNinja.Core.Enums
{
    /// <summary>
    /// Class of the Skype entity.
    /// </summary>
    internal enum SkypeEntityClass
    {
        /// <summary>
        /// Simple chat message (the "messages" table).
        /// </summary>
        ChatMessage = 1,

        /// <summary>
        /// Skype call (the "calls" table).
        /// </summary>
        Call = 2,

        /// <summary>
        /// SMS (the "smses" table).
        /// </summary>
        Sms = 3,

        /// <summary>
        /// File transfer (the "transfers" table).
        /// </summary>
        Transfer = 4,

        /// <summary>
        /// Video message (the "videomessages" table).
        /// </summary>
        VideoMessage = 5,

        /// <summary>
        /// Voice mail (the "voicemails" table).
        /// </summary>
        VoiceMail = 6
    }
}
