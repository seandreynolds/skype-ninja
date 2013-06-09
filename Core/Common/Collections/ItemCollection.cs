using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Caches;

namespace eigenein.SkypeNinja.Core.Common.Collections
{
    /// <summary>
    /// Represents a collection, each element of which has an enumerable type.
    /// </summary>
    public abstract class ItemCollection<TItemType, TItem> : IEnumerable<KeyValuePair<TItemType, IList<TItem>>>
    {
        private readonly Dictionary<TItemType, IList<TItem>> items;

        protected ItemCollection()
        {
            items = new Dictionary<TItemType, IList<TItem>>();
        }

        protected ItemCollection(ItemCollection<TItemType, TItem> collection)
        {
            items = new Dictionary<TItemType, IList<TItem>>(collection.items);
        }

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
                // Check the item count.
                if (!attribute.AllowMultiple && subList.Count != 0)
                {
                    throw new ArgumentException(String.Format(
                        "Multiple items are not allowed for {0}.",
                        itemType));
                }
                // Check the item type.
                if (item.GetType() != attribute.ValueType)
                {
                    throw new ArgumentException(String.Format(
                        "The value type is not valid: {0} ({1} expected).",
                        item.GetType(),
                        attribute.ValueType));
                }
            }
            // Add the item.
            subList.Add(item);
        }

        /// <summary>
        /// Gets the first item of the specified type.
        /// </summary>
        public bool TryGet(TItemType itemType, out TItem item)
        {
            // Get the sublist.
            IList<TItem> subList;
            if (!items.TryGetValue(itemType, out subList) || subList.Count == 0)
            {
                item = default(TItem);
                return false;
            }
            // Return the item.
            item = subList.First();
            return true;
        }

        public void Remove(TItemType itemType)
        {
            IList<TItem> subList;
            if (items.TryGetValue(itemType, out subList))
            {
                subList.Clear();
            }
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
