using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayTech.Shared.Database.Base;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.Shared.DTOs;
using PlayTech.Shared.Utils;

namespace PlayTech.Shared.CQS.Commands
{
    public class BaseSaveCommandAsync<TEntity, TDTO>
        where TEntity : BaseEntity, new()
        where TDTO : BaseDTO, new()
    {
        private readonly IRepository<TEntity> _repository;

        protected BaseSaveCommandAsync(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<int> ExecuteAsync(TDTO model)
        {
            Check.ArgumentNotNull(model, nameof(model), $"{nameof(BaseSaveCommandAsync<TEntity, TDTO>)}.{nameof(ExecuteAsync)}");

            TEntity entity = null;
            if (model.Id == default)
            {
                entity = new TEntity();
                
                await PrepareEntityToInsertAsync(entity, model);
                await _repository.InsertAsync(entity);
            }
            else
            {
                var entities = _repository.GetMany(o => o.Id == model.Id, false);
                entity = await entities.FirstOrDefaultAsync();

                await PrepareEntityToUpdateAsync(entity, model);
                await _repository.UpdateAsync(entity);
            }

            model.Id = entity.Id;

            return entity.Id;
        }

        protected virtual async Task<TEntity> PrepareEntityToInsertAsync(TEntity entity, TDTO model)
        {
            TEntity newEntity = PrepareEntity(entity, model);
            return newEntity;
        }

        protected virtual async Task<TEntity> PrepareEntityToUpdateAsync(TEntity entity, TDTO model)
        {
            TEntity newEntity = PrepareEntity(entity, model);
            return newEntity;
        }

        protected virtual TEntity PrepareEntity(TEntity entity, TDTO model)
        {
            return new TEntity
            {
                Id = model.Id
            };
        }
    }
}
