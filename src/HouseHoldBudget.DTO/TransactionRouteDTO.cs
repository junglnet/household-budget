using HouseholdBudget.Common.Entities;
namespace HouseholdBudget.DTO
{
    public class TransactionRouteDTO : EntityBase
    {

        public string SourceTransactionId {get; set; }
        public string ReceiverTransactionId { get; set; }
        public string SourceFundId { get; set; }
        public string ReceiverFundId { get; set; }
    }
}
