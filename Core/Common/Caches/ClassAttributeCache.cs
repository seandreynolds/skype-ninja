using System;
using System.Collections.Generic;
using System.Linq;

namespace eigenein.SkypeNinja.Core.Common.Caches
{
    /// <summary>
    /// Used to retrieve the attributes.
    /// </summary>
    public static class ClassAttributeCache<TAttribute>
        where TAttribute : Attribute
    {
        private static readonly IDictionary<Type, IList<TAttribute>> Cache = 
            new Dictionary<Type, IList<TAttribute>>();

        public static TAttribute GetAttribute(Type type)
        {
            return GetAttributes(type).First();
        }

        /// <summary>
        /// Gets the class attributes.
        /// </summary>
        public static IList<TAttribute> GetAttributes(Type type)
        {
            IList<TAttribute> attributes;
            if (!Cache.TryGetValue(type, out attributes))
            {
                attributes = type.GetCustomAttributes(
                    typeof(TAttribute),
                    false)
                    .Cast<TAttribute>()
                    .ToList();
                Cache[type] = attributes;
            }
            return attributes;
        }
    }
}
