using System;
using System.Collections.Generic;
using System.Linq;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Caches;

namespace eigenein.SkypeNinja.Core.Connectors.Common.Collections
{
    /// <summary>
    /// Represents a collection, each element of which has an enumerable type.
    /// </summary>
    public class ItemCollection<TItemType, TItem>
    {
        private readonly Dictionary<TItemType, List<TItem>> items =
            new Dictionary<TItemType, List<TItem>>();

        /// <summary>
        /// Adds the item to the collection.
        /// </summary>
        public void Add(TItemType itemType, TItem item)
        {
            FieldValueTypeAttribute attribute;
            if (FieldAttributeCache<TItemType, FieldValueTypeAttribute>.TryGetAttribute(
                itemType.ToString(), 
                out attribute))
            {
                attribute.Validate(item);
            }

            List<TItem> subList;
            if (!items.TryGetValue(itemType, out subList))
            {
                subList = new List<TItem> {item};
                items.Add(itemType, subList);
            }
            subList.Add(item);
        }

        public bool TryGetItem(TItemType itemType, out TItem item)
        {
            List<TItem> subList;
            if (!items.TryGetValue(itemType, out subList) || subList.Count == 0)
            {
                item = default(TItem);
                return false;
            }

            item = subList.First();
            return true;
        }
    }
}
