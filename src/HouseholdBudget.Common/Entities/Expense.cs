using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Расход
    /// </summary>
    public class Expense : DictionaryBase, ITypeTransaction
    {
        public decimal GetOperation(
            IBudgetaryFund SourceBudgetaryFund,
            IBudgetaryFund EndPointBudgetaryFund,
            decimal sum)
        {
            return -sum;
        }

    }
}
