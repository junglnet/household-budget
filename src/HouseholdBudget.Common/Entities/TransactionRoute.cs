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

        public bool IsEqual() =>
            Source.Name == Receiver.Name && Receiver != null ? true : false;
        

        public BudgetaryFund Source { get;  }

        public BudgetaryFund Receiver { get; }

    }
}
