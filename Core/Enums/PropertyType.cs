using System;
using eigenein.SkypeNinja.Core.Common.Attributes;

namespace eigenein.SkypeNinja.Core.Enums
{
    /// <summary>
    /// Message property type.
    /// </summary>
    public enum PropertyType
    {
        /// <summary>
        /// Message creation time.
        /// </summary>
        [FieldValueType(typeof(DateTime))]
        Timestamp
    }
}
