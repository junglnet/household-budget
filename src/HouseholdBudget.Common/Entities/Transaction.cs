using System;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.Common.Entities
{
    public class Transaction : DictionaryBase, IComparable, ICloneable, IMarkToDelete
    {

        public Transaction()
        {
            
            PlannedSum = 0;
            FactSum = 0;
            DateTime = DateTime.Now;
            IsMarkedToDelete = false;

        }

        public TransactionType GetReverseType() =>
            TransactionType.ReverseType;
    

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
        public TransactionType TransactionType { get; set; }
                      
        public bool IsMarkedToDelete { get; set; }

    }
}
