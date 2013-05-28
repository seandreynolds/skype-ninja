using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace eigenein.SkypeNinja.Core.Common.Caches
{
    internal static class FieldAttributeCache<T, TAttribute>
    {
        private static readonly Dictionary<string, List<TAttribute>> Cache =
            new Dictionary<string, List<TAttribute>>();

        /// <summary>
        /// Tries to get the attribute for the specified field.
        /// </summary>
        public static bool TryGetAttribute(string fieldName, out TAttribute attribute)
        {
            List<TAttribute> attributes = GetAttributes(fieldName);
            if (attributes.Count == 1)
            {
                attribute = attributes.First();
                return true;
            }
            if (attributes.Count == 0)
            {
                attribute = default(TAttribute);
                return false;
            }
            throw new ArgumentException(String.Format(
                "Found {0} attributes (the only expected).",
                attributes.Count));
        }

        /// <summary>
        /// Gets all attributes for the specified field.
        /// </summary>
        private static List<TAttribute> GetAttributes(string fieldName)
        {
            lock (Cache)
            {
                List<TAttribute> attributes;

                if (!Cache.TryGetValue(fieldName, out attributes))
                {
                    FieldInfo fieldInfo = typeof(T).GetField(fieldName);
                    if (fieldInfo == null)
                    {
                        throw new ArgumentException(String.Format(
                            "The field is not found: {0}.",
                            fieldName));
                    }

                    attributes = fieldInfo
                        .GetCustomAttributes(typeof(TAttribute), false)
                        .Cast<TAttribute>()
                        .ToList();
                    Cache.Add(fieldName, attributes);
                }

                return attributes;
            }
        }
    }
}
