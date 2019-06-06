using System.Linq;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.DTO
{
    public static class Extentions
    {

        public static BudgetaryFundDTO ToDto(this BudgetaryFund item) => new BudgetaryFundDTO()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            TransactionsId = item.Transactions?.Select(p => p.Id).ToArray()

        };

        public static TransactionDTO ToDto(this Transaction item) => new TransactionDTO()
        {

            Id = item.Id,
            Name = item.Name,
            RelationId = item.RelationId,
            PlannedSum = item.PlannedSum,
            FactSum = item.FactSum,
            DateTime = item.DateTime,
            TypeTransactionId = item.TypeTransaction.Id

        };

    }
}
