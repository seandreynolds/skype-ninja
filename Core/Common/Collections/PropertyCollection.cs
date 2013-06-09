using System;

using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Common.Collections
{
    public class PropertyCollection : ItemCollection<PropertyType, object>
    {
        public PropertyCollection()
        {
            // Do nothing.
        }

        public PropertyCollection(PropertyCollection collection)
            : base(collection)
        {
            // Do nothing.
        }
    }
}
