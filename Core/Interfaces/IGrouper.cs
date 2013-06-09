using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Specifies the grouping rule.
    /// </summary>
    public interface IGrouper
    {
        string GetGroupPart(IMessage message);
    }
}
