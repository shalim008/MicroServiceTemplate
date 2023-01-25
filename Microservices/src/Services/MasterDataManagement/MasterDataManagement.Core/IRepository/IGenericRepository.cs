using MasterDataManagement.Core.Entities;
using MasterDataManagement.Core.Specifications;

namespace MasterDataManagement.Core.IRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(long id);
        Task<IReadOnlyList<T>> ReadOnlyListAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);

        public Task AddAsync(T entity);

        public Task AddRangeAsync(IEnumerable<T> entities);

        public Task Update(T entity);

        public Task UpdateRange(IEnumerable<T> entities);

        public Task Delete(T entity);

        public Task DeleteRange(IEnumerable<T> entities);

        Task<int> Complete();


    }
}