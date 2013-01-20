using System;
using System.Collections.Generic;

namespace eigenein.SkypeNinja.Core.Connectors.Common.Collections
{
    /// <summary>
    /// Represents a collection, each element of which has an enumerable type.
    /// </summary>
    public class ItemCollection<TType, TItem>
    {
        private readonly Dictionary<TType, List<TItem>> items =
            new Dictionary<TType, List<TItem>>();

        /// <summary>
        /// Gets a list of items of the specified type.
        /// </summary>
        public IList<TItem> this[TType itemType]
        {
            get
            {
                lock (items)
                {
                    List<TItem> subItems;
                    if (!items.TryGetValue(itemType, out subItems))
                    {
                        subItems = new List<TItem>();
                        items.Add(itemType, subItems);
                    }
                    return subItems;
                }
            }
        }
    }
}
