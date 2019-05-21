using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITypeTransaction
    {
        decimal GetOperation(Transaction transaction);
    }
}
