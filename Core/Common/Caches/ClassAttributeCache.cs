using System;
using System.Collections.Generic;
using System.Linq;

namespace eigenein.SkypeNinja.Core.Common.Caches
{
    /// <summary>
    /// Used to retrieve the attributes.
    /// </summary>
    internal static class ClassAttributeCache<TClass, TAttribute>
        where TAttribute : Attribute
    {
        private static readonly IList<TAttribute> Attributes;

        static ClassAttributeCache()
        {
            // Preload the attributes.
            Attributes = typeof(TClass).GetCustomAttributes(
                typeof(TAttribute),
                false)
                .Cast<TAttribute>()
                .ToList();
        }

        /// <summary>
        /// Gets the attribute instance.
        /// </summary>
        public static TAttribute GetAttribute()
        {
            if (Attributes.Count == 1)
            {
                return Attributes.First();
            }
            throw new InvalidOperationException(String.Format(
                "Type {0} contains {1} attribute(s).",
                typeof(TClass),
                Attributes.Count));
        }
    }
}
