using System;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    public class Transaction : DictionaryBase, ITransaction, IComparable
    {
        
        public Transaction()
        {
            PlannedSum = 0;
            FactSum = 0;
            DateTime = DateTime.Now;
            SourceBudgetaryFund = null;
            EndPointBudgetaryFund = null;
        }

        public decimal GetOperation() =>
            TypeTransaction.GetOperation(
                SourceBudgetaryFund, 
                EndPointBudgetaryFund, 
                (FactSum !=0)? FactSum : PlannedSum);


        public decimal PlannedSum { get; set; }
        public decimal FactSum { get; set; }
        public DateTime DateTime { get; set; }

        public ITypeTransaction TypeTransaction { get; set; }

        public IBudgetaryFund SourceBudgetaryFund { get; set; }
        public IBudgetaryFund EndPointBudgetaryFund { get; set; }

        public int CompareTo(object o)
        {
            Transaction t = o as Transaction;
            if (t != null)
                return DateTime.CompareTo(t.DateTime);
            else
                throw new Exception("Невозможно сравнить два объекта");
            
        }
    }
}
