using System.Linq.Expressions;

namespace TaskManagementSystem.Core.Interfaces
{
    public interface IGenRepository<TEntity> where TEntity : class
    {
        // Marks as entity as new
        void Add(TEntity entity);

        // Marks as entity as modified
        void Update(TEntity entity);

        // Marks as entity as modified
        void Detach(TEntity entity);

        // Marks as entity as removed
        void Delete(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> where);

        // Get entity by id
        TEntity GetById(long id);

        // Get all entities using Delegate
        TEntity Get(Expression<Func<TEntity, bool>> where);

        // Get all entities by type t
        IEnumerable<TEntity> GetAll();

        // Get entities using delegate
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

        // Get stored procedure data


    }
}
