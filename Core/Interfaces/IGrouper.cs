using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Specifies the grouping rule.
    /// </summary>
    public interface IGrouper
    {
        bool TryGetGroup(IMessage message, out string group);
    }
}
