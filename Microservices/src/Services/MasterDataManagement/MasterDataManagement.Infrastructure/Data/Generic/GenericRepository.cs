using MasterDataManagement.Core.Entities;
using MasterDataManagement.Core.IRepository;
using MasterDataManagement.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace MasterDataManagement.Infrastructure.Data.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }


        public async Task<IReadOnlyList<T>> ReadOnlyListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public virtual Task Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public virtual Task UpdateRange(IEnumerable<T> entities)
        {
            //_context.Set<T>().UpdateRange(entities);

            _context.Set<T>().AttachRange(entities);
            _context.Entry(entities).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public virtual Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);

            return Task.CompletedTask;
        }

        public virtual Task DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);

            return Task.CompletedTask;
        }


        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

    }
}