

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITypeTransaction
    {
        decimal GetOperation(ITransaction transaction);
    }
}
