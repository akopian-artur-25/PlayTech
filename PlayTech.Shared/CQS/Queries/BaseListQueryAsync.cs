using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PlayTech.Shared.CQS.Models;
using PlayTech.Shared.Data;
using PlayTech.Shared.Data.Interfaces;
using PlayTech.Shared.Database.Base;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.Shared.Utils;

namespace PlayTech.Shared.CQS.Queries
{
    public abstract class BaseListQueryAsync<TEntity, TListItemDTO, TFilterDTO>
        where TEntity : BaseEntity, new() 
        where TListItemDTO : class, new()
        where TFilterDTO : BaseListQueryFilter
    {
        private readonly IRepository<TEntity> _repository;

        protected BaseListQueryAsync(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IPagedList<TListItemDTO>> ExecuteAsync(TFilterDTO filter)
        {
            Check.ArgumentNotNull(filter, nameof(filter), $"{nameof(BaseListQueryAsync<TEntity, TListItemDTO, TFilterDTO>)}.{nameof(ExecuteAsync)}");

            var query = _repository.TableNoTracking;

            var filtered = GetListEntityFiltered(filter);
            var filteredQuery = filtered != null ? filtered(query) : query;

            var totalCount = await filteredQuery.CountAsync();

            var includes = GetListEntityIncludes();
            var includableQuery = includes != null ? includes(filteredQuery) : filteredQuery;

            var ordered = GetListEntityOrdered(filter);
            var orderedQuery = ordered != null ? ordered(includableQuery) : filteredQuery;

            var result = await orderedQuery.Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize).Select(ProjectToListItemDTO()).ToListAsync();

            return new PagedList<TListItemDTO>(result, filter.PageIndex, filter.PageSize, totalCount);
        }

        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> GetListEntityFiltered(TFilterDTO filter)
        {
            return null;
        }

        protected virtual Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> GetListEntityIncludes()
        {
            return null;
        }

        protected virtual Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetListEntityOrdered(TFilterDTO filter)
        {
            return null;
        }

        protected virtual Expression<Func<TEntity, TListItemDTO>> ProjectToListItemDTO()
        {
            return item => new TListItemDTO();
        }

    }
}
