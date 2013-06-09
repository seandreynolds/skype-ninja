using System;
using System.Collections.Generic;

using eigenein.SkypeNinja.Core.Common.Groupers;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Common.Collections
{
    public class GrouperCollection : List<IGrouper>
    {
        private static readonly IDictionary<string, IGrouper> Groupers =
            new Dictionary<string, IGrouper>()
            {
                {"author", new AuthorGrouper()},
            };

        public static GrouperCollection FromString(string groups)
        {
            GrouperCollection groupers = new GrouperCollection();
            if (!String.IsNullOrWhiteSpace(groups))
            {
                foreach (string group in groups.Split(','))
                {
                    IGrouper grouper;
                    if (Groupers.TryGetValue(group.ToLowerInvariant(), out grouper))
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
