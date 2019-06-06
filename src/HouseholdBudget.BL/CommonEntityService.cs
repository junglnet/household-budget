using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.BL
{
    public class CommonEntityService<TEntity> : IEntityEditService<TEntity>
    {

        public CommonEntityService(IRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<string> SaveAsync(TEntity entity) =>
           await _entityRepository.AddAsync(entity);

        public async Task<bool> UpdateAsync(TEntity entity) =>
            await _entityRepository.UpdateAsync(entity);

        public async Task<bool> RemoveAsync(string id) =>
            await _entityRepository.DeleteAsync(id);


        private readonly IRepository<TEntity> _entityRepository;

        
    }
}
