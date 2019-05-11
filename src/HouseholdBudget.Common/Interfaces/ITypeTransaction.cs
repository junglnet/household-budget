

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITypeTransaction
    {
        decimal GetOperation(
            IBudgetaryFund SourceBudgetaryFund, 
            IBudgetaryFund EndPointBudgetaryFund, 
            decimal sum);
    }
}
