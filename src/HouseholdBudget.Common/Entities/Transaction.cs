using System;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    public class Transaction : DictionaryBase, IComparable, ICloneable
    {

        public Transaction()
        {
            RelationId = null;
            PlannedSum = 0;
            FactSum = 0;
            DateTime = DateTime.Now;

        }

        public Transaction(string name)
        {
            RelationId = null;
            Name = name;
            PlannedSum = 0;
            FactSum = 0;
            DateTime = DateTime.Now;

        }

        public Transaction(string relationId, string name)
        {
            RelationId = relationId;
            Name = name;
            PlannedSum = 0;
            FactSum = 0;
            DateTime = DateTime.Now;

        }

        /// <summary>
        /// Получение операции транзации
        /// </summary>
        /// <returns></returns>
        public decimal GetOperation() =>
            TypeTransaction.GetOperation(this);


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

        public string RelationId {get; set;}

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
                      


    }
}
