using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITransactionTypeService : IEntityEditService<TransactionType>, IEntityReadService<TransactionType>
    {

    }
}
