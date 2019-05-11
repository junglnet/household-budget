using System.Collections.Generic;
using System;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    public class BudgetaryFund : DictionaryBase, IBudgetaryFund
    {
        public BudgetaryFund()
        {

        }

        public BudgetaryFund(string name)
        {
            Name = name;
        }

        public BudgetaryFund(string id, string name) : base(id, name)
        {

        }

        public List<ITransaction> GetTransactions(DateTime startDate, DateTime endDate) =>
            Transactions.FindAll(d => d.DateTime >= startDate && d.DateTime <= endDate);

        public decimal GetTransactionsSum(DateTime startDate, DateTime endDate)
        {
            List<ITransaction> tmpTransactions = Transactions.FindAll(d => d.DateTime >= startDate && d.DateTime <= endDate);
            if (tmpTransactions.Count == 0) return 0;
            decimal sum = 0;
            foreach(ITransaction ts in tmpTransactions)
            {
                sum = sum + ts.GetOperation();
            }
            return sum;

        }

        public List<ITransaction> Transactions { get; set; }

    }
}
