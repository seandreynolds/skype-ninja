using System;

namespace eigenein.SkypeNinja.Core.Common.Attributes
{
    /// <summary>
    /// Specifies the type of a property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class FieldValueTypeAttribute : Attribute
    {
        public FieldValueTypeAttribute(Type valueType, bool allowMultiple = false)
        {
            this.ValueType = valueType;
            this.AllowMultiple = allowMultiple;
        }

        public Type ValueType
        {
            get;
            private set;
        }

        public bool AllowMultiple
        {
            get;
            private set;
        }
    }
}
