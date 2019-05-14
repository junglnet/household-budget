using System.Collections.Generic;
using System;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    public class BudgetaryFund : DictionaryBase, IBudgetaryFund
    {
        public BudgetaryFund()
        {
            Transactions = new List<ITransaction>();
        }

        public BudgetaryFund(string name)
        {
            Name = name;
            Transactions = new List<ITransaction>();
        }

        public BudgetaryFund(string id, string name) : base(id, name)
        {
            Transactions = new List<ITransaction>();
        }

        /// <summary>
        /// Добавление транзации в бюджетный фонд
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>ИД транзации</returns>
        public string AddTransaction(ITransaction transaction)
        {
            if (transaction == null) return null;
            transaction.Id = Guid.NewGuid().ToString("N");
            Transactions.Add(transaction);
            Transactions.Sort();
            return transaction.Id;
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
