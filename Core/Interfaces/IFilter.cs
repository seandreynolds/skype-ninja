using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    internal interface IFilter
    {
        IPartialQuery ToPartialQuery(IQueryBuilder queryBuilder);
    }
}
