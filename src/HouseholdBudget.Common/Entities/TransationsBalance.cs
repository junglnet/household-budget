
namespace HouseholdBudget.Common.Entities
{
    public class TransationsBalance
    {

        public Transaction Transaction { get; set; }
        public Fund SourceFund {get; set;}

        public Fund ReceiverFund { get; set; }

        public decimal TransationBalance { get; set; }

    }
}
