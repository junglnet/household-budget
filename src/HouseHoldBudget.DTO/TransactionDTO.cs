using System;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.DTO
{
    public class TransactionDTO : DictionaryBase
    {
        public string RelationId { get; set; }
        public decimal PlannedSum { get; set; }
        public decimal FactSum { get; set; }
        public DateTime DateTime { get; set; }
        public string TypeTransactionId { get; set; }
    }
}
