using System;

using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Common.Groupers
{
    /// <summary>
    /// Groups messages by the author property.
    /// </summary>
    internal class AuthorGrouper : IGrouper
    {
        public string GetGroupPart(IMessage message)
        {
            object authorProperty;
            if (!message.Properties.TryGet(PropertyType.Author, out authorProperty))
            {
                throw new ArgumentException("The message has no author.");
            }
            string author = (string)authorProperty;
            return author;
        }
    }
}
