using System;
using System.Collections;
using System.Collections.Generic;

namespace eigenein.SkypeNinja.Core.Connectors.Common
{
    /// <summary>
    /// Message path within the store.
    /// </summary>
    public class MessageGroup : IEnumerable<string>
    {
        private readonly IList<string> parts = new List<string>();

        public void AddPart(string part)
        {
            parts.Add(part);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return parts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
