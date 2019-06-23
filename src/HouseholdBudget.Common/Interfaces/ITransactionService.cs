using System.Threading.Tasks;
using System.Collections.Generic;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITransactionService : IEntityEditService<Transaction>, IEntityReadService<Transaction>
    {


    }
}
