using System;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    public class Transaction : DictionaryBase, IComparable, ICloneable
    {
        
        public Transaction()
        {
            Id = null;
            PlannedSum = 0;
            FactSum = 0;
            DateTime = DateTime.Now;
            SourceBudgetaryFund = null;
            EndPointBudgetaryFund = null;
        }

        public Transaction(string name)
        {
            Id = null;
            Name = name;
            PlannedSum = 0;
            FactSum = 0;
            DateTime = DateTime.Now;
            SourceBudgetaryFund = null;
            EndPointBudgetaryFund = null;
        }

        public Transaction(string id, string name)
        {
            Id = id;
            Name = name;
            PlannedSum = 0;
            FactSum = 0;
            DateTime = DateTime.Now;
            SourceBudgetaryFund = null;
            EndPointBudgetaryFund = null;
        }

        /// <summary>
        /// Получение операции транзации
        /// </summary>
        /// <returns></returns>
        public decimal GetOperation() =>
            TypeTransaction.GetOperation(this);

        /// <summary>
        /// Сохранение транзакции
        /// </summary>
        public void SaveTransaction() =>
            TransactionSaver.SaveTransaction(this);

        public object Clone() =>
            this.MemberwiseClone();

        public int CompareTo(object o)
        {
            Transaction t = o as Transaction;
            if (t != null)
                return DateTime.CompareTo(t.DateTime);
            else
                throw new Exception("Невозможно сравнить два объекта");

        }

        /// <summary>
        /// Плановая сумма транзации
        /// </summary>
        public decimal PlannedSum { get; set; }

        /// <summary>
        /// Фактическая сумма транзакции
        /// </summary>
        public decimal FactSum { get; set; }

        /// <summary>
        /// Дата транзации
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Тип транзации
        /// </summary>
        public ITypeTransaction TypeTransaction { get; set; }

        /// <summary>
        /// Сохранятель транзации
        /// </summary>
        public ITransactionSaver TransactionSaver { get; set; }

        /// <summary>
        /// Истоник транзакции
        /// </summary>
        public BudgetaryFund SourceBudgetaryFund { get; set; }
         
        /// <summary>
        /// Получатель транзации
        /// </summary>
        public BudgetaryFund EndPointBudgetaryFund { get; set; }

         


    }
}
