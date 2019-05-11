using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Приход
    /// </summary>
    public class Income : DictionaryBase, ITypeTransaction
    {
        public decimal GetOperation(
            IBudgetaryFund SourceBudgetaryFund,
            IBudgetaryFund EndPointBudgetaryFund,
            decimal sum)
        {
            return sum;
        }
    }
}
