using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.BL
{
    /// <summary>
    /// Класс описывает слой бизнес-логики транзакции
    /// </summary>
    public class TransactionLayer
    {
        public TransactionLayer(IRepository<Transaction> transationRepository)
        {
            _transationRepository = transationRepository;
        }

        public async Task<string> AddAsync(Transaction item,
            BudgetaryFund sourceBF, BudgetaryFund receiverBF)
        {
            item.RelationId = Guid.NewGuid().ToString("N");

            return await item.TypeTransaction.AddAsync(item, sourceBF, receiverBF, _transationRepository);

        }

        public async Task<bool> UpdateAsync(Transaction item, 
            BudgetaryFund sourceBF, BudgetaryFund receiverBF) =>
                await item.TypeTransaction.UpdateAsync(item, sourceBF, receiverBF, _transationRepository);

        public async Task<bool> DeleteAsync(Transaction item,
            BudgetaryFund sourceBF, BudgetaryFund receiverBF) =>
                await item.TypeTransaction.DeleteAsync(item, sourceBF, receiverBF, _transationRepository);
        


       



        private readonly IRepository<Transaction> _transationRepository;

    }
}
