using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Фиксированное сальдо
    /// </summary>
    public class Balance : DictionaryBase, ITypeTransaction
    {
        public decimal GetOperation(ITransaction transaction)
        {
            return (transaction.FactSum != 0) ? transaction.FactSum : transaction.PlannedSum;
        }

    }
}
