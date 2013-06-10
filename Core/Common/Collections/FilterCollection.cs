using System;
using System.Collections.Generic;

using eigenein.SkypeNinja.Core.Common.Extensions;
using eigenein.SkypeNinja.Core.Common.Filters;
using eigenein.SkypeNinja.Core.Common.Helpers;
using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Common.Collections
{
    public class FilterCollection : ItemCollection<FilterType, object>
    {
        private delegate void HandleFilterMethod(
            FilterCollection filters, 
            string specifierValue);

        private static readonly IDictionary<string, HandleFilterMethod> FilterHandlers =
            new Dictionary<string, HandleFilterMethod>()
            {
                {FilterSpecifier.Sort, HandleSortFilter},
            };

        public static IEnumerable<string> Specifiers
        {
            get
            {
                return FilterHandlers.Keys;
            }
        }

        public static FilterCollection FromString(string filtersQuery)
        {
            // Initialize the empty collection.
            FilterCollection filters = new FilterCollection();
            if (!String.IsNullOrWhiteSpace(filtersQuery))
            {
                // Parse the filters string.
                QueryParameters parameters = UriExtensions.ParseQuery(filtersQuery);
                // Iterate over the parameters and add the filters.
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                }
            }
            // Return the filters.
            return filters;
        }

        private static void HandleSortFilter(
            FilterCollection filters,
            string specifierValue)
        {
            filters.Add(FilterType.Sort, SortFilter.FromString(specifierValue));
        }
    }
}
