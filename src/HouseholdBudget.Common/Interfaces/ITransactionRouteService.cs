using HouseholdBudget.Common.Entities;
using System.Threading.Tasks;

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITransactionRouteService : IEntityEditService<TransactionRoute>
    {

        Task<TransactionRoute> FindByTransactionId(string id);
    }
}
