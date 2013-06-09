using System;
using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Connectors.Common;

namespace eigenein.SkypeNinja.Core.Enums
{
    /// <summary>
    /// Message property type.
    /// </summary>
    public enum PropertyType
    {
        /// <summary>
        /// Message class.
        /// </summary>
        [FieldValueType(typeof(MessageClass))]
        MessageClass,

        /// <summary>
        /// Message creation time.
        /// </summary>
        [FieldValueType(typeof(DateTime))]
        Timestamp,

        /// <summary>
        /// Message body.
        /// </summary>
        [FieldValueType(typeof(string))]
        Body,

        /// <summary>
        /// Message author.
        /// </summary>
        [FieldValueType(typeof(string))]
        Author,

        /// <summary>
        /// Message author display name.
        /// </summary>
        [FieldValueType(typeof(string))]
        FromDisplayName,

        /// <summary>
        /// Message group (if grouping is used).
        /// </summary>
        [FieldValueType(typeof(MessageGroup))]
        Group,

        /// <summary>
        /// Skype message status.
        /// </summary>
        [FieldValueType(typeof(SkypeChatMessageStatus))]
        SkypeMessageStatus,

        /// <summary>
        /// Skype message type.
        /// </summary>
        [FieldValueType(typeof(SkypeMessageType))]
        SkypeMessageType
    }
}
