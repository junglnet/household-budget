using HouseholdBudget.Common.Entities;
using System.Threading.Tasks;

namespace HouseholdBudget.Common.Interfaces
{
    public interface ITypeTransaction
    {
        decimal GetOperation (Transaction transaction);

        Task <string> AddAsync (
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository);

        Task <bool> UpdateAsync (
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository);

        Task<bool> DeleteAsync (
            Transaction transaction, 
            BudgetaryFund sourceBF, 
            BudgetaryFund receiverBF, 
            IRepository<Transaction> transationRepository);
    }

}
