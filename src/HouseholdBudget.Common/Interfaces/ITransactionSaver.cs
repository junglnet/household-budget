

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITransactionSaver
    {
        void SaveTransaction(ITransaction transaction);
    }
}
