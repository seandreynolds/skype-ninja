using System;
using System.Collections.Generic;

using eigenein.SkypeNinja.Core.Common.Groupers;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Common.Collections
{
    public class GrouperCollection : List<IGrouper>
    {
        private static readonly IDictionary<string, IGrouper> Groupers =
            new Dictionary<string, IGrouper>()
            {
                {GrouperSpecifier.Author, new AuthorGrouper()},
            };

        public static IEnumerable<string> Specifiers
        {
            get
            {
                return Groupers.Keys;
            }
        }

        public static GrouperCollection FromString(string groups)
        {
            GrouperCollection groupers = new GrouperCollection();
            if (!String.IsNullOrWhiteSpace(groups))
            {
                foreach (string group in groups.Split(','))
                {
                    IGrouper grouper;
                    if (Groupers.TryGetValue(group.Trim().ToLowerInvariant(), out grouper))
                    {
                        groupers.Add(grouper);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("Invalid grouper: {0}.", @group));
                    }
                }
            }
            return groupers;
        }
    }
}
