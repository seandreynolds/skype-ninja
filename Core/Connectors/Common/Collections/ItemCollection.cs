using System;
using System.Collections.Generic;
using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Helpers;

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
                itemType.ToString(), out attribute))
            {
                attribute.Validate(item);
            }

            GetSubList(itemType).Add(item);
        }

        /// <summary>
        /// Gets a list of items of the specified type.
        /// </summary>
        private List<TItem> GetSubList(TItemType itemType)
        {
            lock (items)
            {
                List<TItem> subList;
                if (!items.TryGetValue(itemType, out subList))
                {
                    subList = new List<TItem>();
                    items.Add(itemType, subList);
                }
                return subList;
            }
        }
    }
}
