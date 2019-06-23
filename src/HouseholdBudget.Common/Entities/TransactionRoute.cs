using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    public class TransactionRoute : EntityBase
    {

        public bool IsEqualFunds() =>
            SourceFund.Name == ReceiverFund.Name && ReceiverFund != null ? true : false;
        
        public Transaction SourceTransaction { get; set; }
        public Transaction ReceiverTransaction { get; set; }
        public Fund SourceFund { get; set; }

        public Fund ReceiverFund { get; set; }

    }
}
