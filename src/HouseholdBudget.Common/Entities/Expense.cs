using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Расход
    /// </summary>
    public class Expense : DictionaryBase, ITypeTransaction, ITransactionSaver
    {
        public decimal GetOperation(Transaction transaction)
        {
            return (transaction.FactSum != 0) ? -transaction.FactSum : -transaction.PlannedSum;
        }

        /// <summary>
        /// Сохранение транзакции
        /// </summary>
        /// <param name="transaction"></param>
        public void SaveTransaction(Transaction transaction)
        {

            
            if (
                transaction.EndPointBudgetaryFund == transaction.SourceBudgetaryFund ||
                transaction.EndPointBudgetaryFund == null)
            {
                if (transaction.Id == null || 
                    transaction.SourceBudgetaryFund.FindTransationById(transaction.Id) == null)
                {
                    transaction.SourceBudgetaryFund.AddTransaction(transaction);
                }
                
            }

            else
            {

                Transaction incomeTransation;
                incomeTransation = (Transaction)transaction.Clone();

                incomeTransation.EndPointBudgetaryFund = null;
                incomeTransation.SourceBudgetaryFund = transaction.EndPointBudgetaryFund;
                incomeTransation.TypeTransaction = new Income();
                incomeTransation.TransactionSaver = new Income();

                transaction.EndPointBudgetaryFund.AddTransaction(incomeTransation);
                transaction.SourceBudgetaryFund.AddTransaction(transaction);

            }

        }
    }
}
