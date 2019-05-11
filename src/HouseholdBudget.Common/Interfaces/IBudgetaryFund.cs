using System.Collections.Generic;
using System;

namespace HouseholdBudget.Common.Interfaces
{
    public interface IBudgetaryFund
    {
        string Name { get; set; }

        List<ITransaction> Transactions { get; set; }
        List<ITransaction> GetTransactions(DateTime startDate, DateTime endDate);
        decimal GetTransactionsSum(DateTime startDate, DateTime endDate);
    }
}
