using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    public class TransactionRoute
    {

        public TransactionRoute(BudgetaryFund source, BudgetaryFund receiver)
        {

            Source = source;
            Receiver = receiver;

        }

        public int GetDistance() =>
            Source.Name == Receiver.Name ? 0 : 1;
        

        public BudgetaryFund Source { get;  }

        public BudgetaryFund Receiver { get; }

    }
}
