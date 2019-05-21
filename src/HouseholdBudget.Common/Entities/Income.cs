using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Приход
    /// </summary>
    public class Income : DictionaryBase, ITypeTransaction, ITransactionSaver
    {
        public decimal GetOperation(Transaction transaction)
        {
            return (transaction.FactSum != 0) ? transaction.FactSum : transaction.PlannedSum;
        }

        /// <summary>
        /// Сохранение транзакции
        /// </summary>
        /// <param name="transaction"></param>
        public void SaveTransaction(Transaction transaction)
        {
            if (
                transaction.SourceBudgetaryFund == transaction.EndPointBudgetaryFund || 
                transaction.SourceBudgetaryFund == null) {
                transaction.EndPointBudgetaryFund.AddTransaction(transaction);
            }

            else
            {
                
                Transaction expenseTransation;
                expenseTransation = (Transaction)transaction.Clone();

                expenseTransation.EndPointBudgetaryFund = transaction.SourceBudgetaryFund;
                expenseTransation.SourceBudgetaryFund = null;
                expenseTransation.TypeTransaction = new Expense();
                expenseTransation.TransactionSaver = new Expense();

                transaction.EndPointBudgetaryFund.AddTransaction(transaction);
                transaction.SourceBudgetaryFund.AddTransaction(expenseTransation);
            }
                  
        }
    }
}
