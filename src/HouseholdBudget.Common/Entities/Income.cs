using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Приход
    /// </summary>
    public class Income : DictionaryBase, ITypeTransaction, ITransactionSaver
    {
        public decimal GetOperation(ITransaction transaction)
        {
            return (transaction.FactSum != 0) ? transaction.FactSum : transaction.PlannedSum;
        }

        public void SaveTransaction(ITransaction transaction)
        {
            if (
                transaction.SourceBudgetaryFund == transaction.EndPointBudgetaryFund || 
                transaction.SourceBudgetaryFund == null) {
                transaction.EndPointBudgetaryFund.Transactions.Add(transaction);
            }
                  
        }
    }
}
