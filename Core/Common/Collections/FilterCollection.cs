using System;

using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Common.Collections
{
    public class FilterCollection : ItemCollection<FilterType, object>
    {
        public static FilterCollection FromString(string filters)
        {
            // TODO
            return new FilterCollection();
        }
    }
}
