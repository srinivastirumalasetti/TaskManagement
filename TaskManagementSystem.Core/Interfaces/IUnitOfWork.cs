using TaskManagementSystem.Core.Interfaces;

namespace TaskManagementSystem.Infrastructure.TMSData.Interfaces
{
    public interface IUnitOfWork
    {
        IGenRepository<TEntity> repository<TEntity>() where TEntity : class;
        void SaveChanges();
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        void Rollback();

        void BeginTransaction(System.Data.IsolationLevel isolationlevel = System.Data.IsolationLevel.Unspecified);

        bool Commit();
    }
}
