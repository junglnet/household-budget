using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITransactionSaver
    {
        void SaveTransaction(Transaction transaction);
    }
}
