using System;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Filters;

namespace eigenein.SkypeNinja.Core.Enums
{
    public enum FilterType
    {
        /// <summary>
        /// Specifies the fields and sort order.
        /// </summary>
        [FieldValueType(typeof(SortFilter))]
        Sort
    }
}
