using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common
{
    public abstract class Factory
    {
        public static Factory Current { get; protected set; }
        public abstract IRepository<BudgetaryFund> BudgetaryFundRepository { get; }
        public abstract IRepository<Transaction> TransactionRepository { get; }
        public abstract IRepository<ITypeTransaction> TypeTransactionRepository { get; }

    }
}
