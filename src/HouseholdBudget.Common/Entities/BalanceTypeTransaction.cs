using System.Threading.Tasks;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Фиксированное сальдо
    /// </summary>
    public class BalanceTypeTransaction : DictionaryBase, ITypeTransaction
    {

        public decimal GetOperation(Transaction transaction) =>
            (transaction.FactSum != 0) ? transaction.FactSum : transaction.PlannedSum;

        public void AddToRoute(Transaction transaction, TransactionRoute route) =>
            route.Source.Transactions.Add(transaction);


        public ITypeTransaction GetRelationType() =>
            new ExpenseTypeTransaction();









        public async Task<string> AddAsync(
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository)
        {

            sourceBF.Transactions.Add(transaction);

            return await transationRepository.AddAsync(transaction);

        }

        public async Task<bool> DeleteAsync(
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository)
        {

            sourceBF.Transactions.Remove(sourceBF.Transactions.Find(item => item.RelationId == transaction.RelationId));

            return await transationRepository.DeleteAsync(transaction.Id) ? true : false;

        }

        
        public async Task<bool> UpdateAsync(
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository)
        {
            sourceBF.Transactions.Remove(sourceBF.Transactions.Find(item => item.RelationId == transaction.RelationId));

            sourceBF.Transactions.Add(transaction);

            return await transationRepository.UpdateAsync(transaction);
        }
    }
}
