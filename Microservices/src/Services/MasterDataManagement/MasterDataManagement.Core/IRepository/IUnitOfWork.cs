using MasterDataManagement.Core.Entities;

namespace MasterDataManagement.Core.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}