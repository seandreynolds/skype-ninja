using System;
using System.Collections;
using System.Collections.Generic;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Caches;

namespace eigenein.SkypeNinja.Core.Common.Collections
{
    /// <summary>
    /// Represents a collection, each element of which has an enumerable type.
    /// </summary>
    public class ItemCollection<TItemType, TItem> : IEnumerable<KeyValuePair<TItemType, IList<TItem>>>
    {
        private readonly Dictionary<TItemType, IList<TItem>> items =
            new Dictionary<TItemType, IList<TItem>>();

        /// <summary>
        /// Adds the item to the collection.
        /// </summary>
        public void Add(TItemType itemType, TItem item)
        {
            // Initialize the value list.
            IList<TItem> subList;
            if (!items.TryGetValue(itemType, out subList))
            {
                subList = new List<TItem>();
                items[itemType] = subList;
            }

            // Validate the value being added.
            FieldValueTypeAttribute attribute;
            if (FieldAttributeCache<TItemType, FieldValueTypeAttribute>.TryGetAttribute(
                itemType.ToString(), 
                out attribute))
            {
                if (!attribute.AllowMultiple && subList.Count != 0)
                {
                    throw new ArgumentException(String.Format(
                        "Multiple items are not allowed for {0}.",
                        itemType));
                }
                if (item.GetType() != attribute.ValueType)
                {
                    throw new ArgumentException(String.Format(
                        "The value type is not valid: {0} ({1} expected).",
                        item.GetType(),
                        attribute.ValueType));
                }
            }

            subList.Add(item);
        }

        public IEnumerator<KeyValuePair<TItemType, IList<TItem>>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
