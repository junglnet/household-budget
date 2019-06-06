using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseholdBudget.Common.Interfaces
{
    public interface IEntityReadService<TEntity>
    {

        Task<TEntity> GetOneAsync(string id);
        Task<IReadOnlyList<TEntity>> GetManyAsync(string[] ids);

    }
}
