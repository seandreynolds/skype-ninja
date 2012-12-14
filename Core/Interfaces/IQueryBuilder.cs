using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    internal interface IQueryBuilder
    {
        // TODO.
    }

    internal interface IQueryBuilder<TQuery> : IQueryBuilder
    {
        TQuery Build(IPartialQuery partialQuery);
    }
}
