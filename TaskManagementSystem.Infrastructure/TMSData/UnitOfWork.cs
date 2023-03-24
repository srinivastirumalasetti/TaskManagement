using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;
using System.Transactions;
using TaskManagementSystem.Core.Interfaces;
using TaskManagementSystem.Infrastructure.Models;
using TaskManagementSystem.Infrastructure.Repository;
using TaskManagementSystem.Infrastructure.TMSData.Interfaces;
using System.Data;

namespace TaskManagementSystem.Infrastructure.TMSData
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly IDBFactory _dbFactory;
        private TaskManagementDbContext _dbContext;
        private IDbContextTransaction _transaction = null;
        private Hashtable _repositories = null;

        //public TaskManagementDbContext DBContext
        //{
        //    get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        //}

        public UnitOfWork(TaskManagementDbContext DBContext)
        {
            //this._dbFactory = dbfactory;
            this._dbContext = DBContext;
        }
        
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _transaction.Dispose();
            _dbContext.Dispose();
        }
        public void BeginTransaction(System.Data.IsolationLevel isolationlevel = System.Data.IsolationLevel.Unspecified)
        {
            _transaction = _dbContext.Database.BeginTransaction(isolationlevel);
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }


        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }

        public IGenRepository<TEntity> repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();
            var Type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(GenRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)), _dbContext);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IGenRepository<TEntity>)_repositories[Type];
        }
    }


}
