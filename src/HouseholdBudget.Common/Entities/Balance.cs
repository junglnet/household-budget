using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Фиксированное сальдо
    /// </summary>
    public class Balance : DictionaryBase, ITypeTransaction
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
