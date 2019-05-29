using HouseholdBudget.Common.Interfaces;
using System.Threading.Tasks;

namespace HouseholdBudget.Common.Entities
{
    /// <summary>
    /// Класс описывает тип транзакции: Расход
    /// </summary>
    public class ExpenseTypeTransaction : ITypeTransaction
    {
        public decimal GetOperation(Transaction transaction) =>
            (transaction.FactSum != 0) ? -transaction.FactSum : -transaction.PlannedSum;

        public void AddToRoute(Transaction transaction, TransactionRoute route) =>
            route.Source.Transactions.Add(transaction);
               

        public ITypeTransaction GetRelationType() =>
            new IncomeTypeTransaction();
        



        public async Task<string> AddAsync(
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository)
        {
            
            if (sourceBF.Name != receiverBF.Name)
            {
                Transaction relationTransaction = (Transaction)transaction.Clone();

                relationTransaction.TypeTransaction = new IncomeTypeTransaction();

                sourceBF.Transactions.Add(transaction);

                receiverBF.Transactions.Add(relationTransaction);

                await transationRepository.AddAsync(relationTransaction);
            }
                        
            sourceBF.Transactions.Add(transaction);

            return await transationRepository.AddAsync(transaction);            
                                    
        }

        public async Task<bool> UpdateAsync (
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository)
        {

            bool isUpdatedReceiverTransaction = true;

            if (sourceBF.Name != receiverBF.Name)
            {

              //  receiverBF.Transactions.Remove(
                 //   receiverBF.Transactions.Find(item => item.RelationId == transaction.RelationId)
                    );

                Transaction relationTransaction = (Transaction)transaction.Clone();

                relationTransaction.TypeTransaction = new IncomeTypeTransaction();

                receiverBF.Transactions.Add(relationTransaction);

                isUpdatedReceiverTransaction = await transationRepository.UpdateAsync(relationTransaction);

            }                

          //  sourceBF.Transactions.Remove(sourceBF.Transactions.Find(item => item.RelationId == transaction.RelationId));

            sourceBF.Transactions.Add(transaction);

            return isUpdatedReceiverTransaction && await transationRepository.UpdateAsync(transaction) ? true : false;
                       

        }

        public async Task<bool> DeleteAsync (
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository)
        {

            bool isDeletedReceiverTransaction = true;

            if (sourceBF.Name != receiverBF.Name)
            {
                receiverBF.Transactions.Remove(
                 //   receiverBF.Transactions.Find(item => item.RelationId == transaction.RelationId));

             //   isDeletedReceiverTransaction = await transationRepository.DeleteAsync(transaction.Id);
            }
               

          //  sourceBF.Transactions.Remove(sourceBF.Transactions.Find(item => item.RelationId == transaction.RelationId));

            return isDeletedReceiverTransaction && await transationRepository.DeleteAsync(transaction.Id) ? true : false;


        }


    }
}
