using System.Threading.Tasks;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Приход
    /// </summary>
    public class IncomeTypeTransaction : DictionaryBase, ITypeTransaction
    {

        public decimal GetOperation(Transaction transaction) =>
            (transaction.FactSum != 0) ? transaction.FactSum : transaction.PlannedSum;

        public ITypeTransaction GetReverseType() =>
            new ExpenseTypeTransaction();

    }
}
