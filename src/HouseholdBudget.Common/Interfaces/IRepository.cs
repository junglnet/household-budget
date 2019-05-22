using System.Threading.Tasks;
using System.Collections.Generic;
namespace HouseholdBudget.Common.Interfaces
{
    public interface IRepository<T>
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id);

        Task<IReadOnlyList<T>> GetByIdsAsync(string[] ids);

        Task<string> AddAsync(T item);

        Task<bool> UpdateAsync(T item);

        Task<bool> DeleteAsync(string id);

        Task<bool> IsExistById(string id);
    }
}
