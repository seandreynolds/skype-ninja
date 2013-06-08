using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Represents a filtering rule.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Checks whether the message matches the filter.
        /// </summary>
        bool Matches(IMessage message);
    }
}
