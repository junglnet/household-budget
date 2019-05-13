using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Расход
    /// </summary>
    public class Expense : DictionaryBase, ITypeTransaction
    {
        public decimal GetOperation(ITransaction transaction)
        {
            return (transaction.FactSum != 0) ? -transaction.FactSum : -transaction.PlannedSum;
        }

    }
}
