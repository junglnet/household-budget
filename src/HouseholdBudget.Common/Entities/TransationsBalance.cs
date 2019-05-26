
namespace HouseholdBudget.Common.Entities
{
    public class TransationsBalance
    {

        public Transaction Transaction { get; set; }
        public BudgetaryFund SourceBudgetaryFund {get; set;}

        public BudgetaryFund ReceiverBudgetaryFund { get; set; }

        public decimal TransationBalance { get; set; }

    }
}
