using HouseholdBudget.Common.Interfaces;
using System.Threading.Tasks;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Расход
    /// </summary>
    public class ExpenseTypeTransaction : DictionaryBase, ITypeTransaction
    {
        public decimal GetOperation(Transaction transaction) =>
            (transaction.FactSum != 0) ? -transaction.FactSum : -transaction.PlannedSum;
                       

        public ITypeTransaction GetReverseType() =>
            new IncomeTypeTransaction();
        
        

    }
}
