using System;

namespace eigenein.SkypeNinja.Core.Common.Attributes
{
    /// <summary>
    /// Specifies the type of a property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class FieldValueTypeAttribute : Attribute
    {
        private readonly Type valueType;

        public FieldValueTypeAttribute(Type valueType)
        {
            this.valueType = valueType;
        }

        /// <summary>
        /// Validates that the value has the expected type.
        /// </summary>
        public void Validate(object value)
        {
            if (value.GetType() != valueType)
            {
                throw new ArgumentException(String.Format(
                    "The value type is not valid: {0} ({1} expected).",
                    value.GetType(),
                    valueType));
            }
        }
    }
}
