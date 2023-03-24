using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Linq.Expressions;
using TaskManagementSystem.Core.Interfaces;
using TaskManagementSystem.Infrastructure.Models;
using TaskManagementSystem.Infrastructure.TMSData.Interfaces;

namespace TaskManagementSystem.Infrastructure.Repository
{
    public class GenRepository<TEntity> : IGenRepository<TEntity> where TEntity : class
    {
        private TaskManagementDbContext _dbContext = null;
        private readonly DbSet<TEntity> _dbset;

        //protected IDBFactory DbFactory
        //{
        //    get;
        //    private set;
        //}

        //protected TaskManagementDbContext DbContext
        //{
        //    get { return _dbContext ?? (_dbContext = DbFactory.Init()); }
        //}

        public GenRepository(TaskManagementDbContext DbContext)
        {
            //DbFactory = dbFactory;
            _dbContext = DbContext;
            _dbset = DbContext.Set<TEntity>();
        }
        public virtual void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Detach(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public virtual void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = _dbset.Where(where).AsEnumerable();
            _dbContext.Set<TEntity>().RemoveRange(objects);
        }

        public virtual TEntity GetById(long id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbset.ToList();
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _dbset.ToListAsync();
            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var result = await _dbset.Where(where).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       


    }
}
