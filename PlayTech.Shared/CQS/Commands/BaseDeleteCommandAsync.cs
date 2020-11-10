using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayTech.Shared.Database.Base;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.Shared.Utils;

namespace PlayTech.Shared.CQS.Commands
{
    public class BaseDeleteCommandAsync<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly IRepository<TEntity> _repository;

        protected BaseDeleteCommandAsync(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<int> ExecuteAsync(int id)
        {
            var entity = await _repository.GetSingleAsync(o => o.Id == id, false);

            await BeforeDeleteAsync(entity);
            
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
            }

            return entity.Id;
        }

        public virtual async Task BeforeDeleteAsync(TEntity entity)
        {
        }
    }
}
