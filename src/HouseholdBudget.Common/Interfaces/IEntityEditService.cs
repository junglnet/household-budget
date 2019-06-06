using System.Threading.Tasks;

namespace HouseholdBudget.Common.Interfaces
{
    public interface IEntityEditService<T>
    {
        Task<string> SaveAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> RemoveAsync(string id);

    }
}
