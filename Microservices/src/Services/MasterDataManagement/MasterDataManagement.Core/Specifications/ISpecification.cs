using System.Linq.Expressions;

namespace MasterDataManagement.Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        //List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }

        Expression<Func<T, object>> GroupBy { get; }

        Expression<Func<T, object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnabled { get; }
    }
}