using System.Collections.Generic;
using System;
using HouseholdBudget.Common.Interfaces;
namespace HouseholdBudget.Common.Entities
{
    public class BudgetaryFund : DictionaryBase
    {
        public BudgetaryFund()
        {
            Transactions = new List<Transaction>();
        }

        public BudgetaryFund(string name)
        {
            Name = name;
            Transactions = new List<Transaction>();
        }

        public BudgetaryFund(string id, string name) : base(id, name)
        {
            Transactions = new List<Transaction>();
        }

        /// <summary>
        /// Добавление транзации в бюджетный фонд
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>ИД транзации</returns>
        public string AddTransaction(Transaction transaction)
        {
            if (transaction == null) return null;

            transaction.Id = Guid.NewGuid().ToString("N");
            Transactions.Add(transaction);
            Transactions.Sort();
            return transaction.Id;
        }

        public Transaction FindTransationById(string id)
        {
            return new Transaction();
        }

        public List<Transaction> GetTransactions(DateTime startDate, DateTime endDate) =>
            Transactions.FindAll(d => d.DateTime >= startDate && d.DateTime <= endDate);

        public decimal GetTransactionsSum(DateTime startDate, DateTime endDate)
        {
            List<Transaction> tmpTransactions = Transactions.FindAll(d => d.DateTime >= startDate && d.DateTime <= endDate);

            if (tmpTransactions.Count == 0) return 0;
            decimal sum = 0;
            foreach(Transaction ts in tmpTransactions)
            {
                sum = sum + ts.GetOperation();
            }
            return sum;

        }

        public List<Transaction> Transactions { get; set; }

    }
}
