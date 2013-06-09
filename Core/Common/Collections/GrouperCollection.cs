using System;
using System.Collections.Generic;

using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Common.Collections
{
    public class GrouperCollection : List<IGrouper>
    {
        public static GrouperCollection FromString(string groups)
        {
            // TODO.
            return new GrouperCollection();
        }
    }
}
