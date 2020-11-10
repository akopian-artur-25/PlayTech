using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlayTech.Shared.Database.Interfaces
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {

        #region Methods

        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            bool isDisableTracking = true);

        IQueryable<TResult> GetMany<TResult>(Expression<Func<TEntity, bool>> predicate = null,
           bool isDisableTracking = true) where TResult : class;

        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate = null,
            bool isDisableTracking = true);

        TResult GetSingle<TResult>(Expression<Func<TEntity, bool>> predicate = null,
            bool isDisableTracking = true) where TResult : class;

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate = null,
           bool isDisableTracking = true);

        Task<TResult> GetSingleAsync<TResult>(Expression<Func<TEntity, bool>> predicate = null,
            bool isDisableTracking = true) where TResult : class;


        bool Any(Expression<Func<TEntity, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

        bool All(Expression<Func<TEntity, bool>> expression);

        Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<TEntity> entities);
        Task InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);
        Task UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<TEntity> entities);
        Task DeleteAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        #endregion

    }
}
