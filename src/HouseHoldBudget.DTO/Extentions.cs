using System.Linq;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.DTO
{
    public static class Extentions
    {

        public static FundDTO ToDto(this Fund item) => new FundDTO()
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
            PlannedSum = item.PlannedSum,
            FactSum = item.FactSum,
            DateTime = item.DateTime,
            TransactionTypeId = item.TransactionType.Id

        };

        public static TransactionRouteDTO ToDto(this TransactionRoute item) => new TransactionRouteDTO()
        {
            Id = item.Id,
            SourceTransactionId = item.SourceTransaction.Id,
            ReceiverTransactionId = item.ReceiverTransaction?.Id,
            SourceFundId = item.SourceFund.Id,
            ReceiverFundId = item.ReceiverFund?.Id
        };

        public static TransactionTypeDTO ToDto(this TransactionType item) => new TransactionTypeDTO()
        {

            Id = item.Id,
            Name = item.Name,
            ReverseTypeId = item.ReverseType == null ? null : item.ReverseType.Id,
            TypeStatusId = (int)item.TypeStatus

        };

    }
}
