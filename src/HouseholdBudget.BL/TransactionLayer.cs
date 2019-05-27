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
        public TransactionLayer(
            IRepository<Transaction> transationRepository, 
            IRepository<BudgetaryFund> budgetaryFundRepository)

        {
            _transationRepository = transationRepository;
            _budgetaryFundRepository = budgetaryFundRepository;
        }

        public async Task<string> AddAsync(
            Transaction item,
            TransactionRoute route)

        {
            item.RelationId = Guid.NewGuid().ToString("N");

            if (route.GetDistance() == 1) {

                Transaction relationTransaction = (Transaction)item.Clone();
                relationTransaction.TypeTransaction = item.TypeTransaction.GetRelationType();                
                relationTransaction.TypeTransaction.AddToRoute(relationTransaction, route);
                await _transationRepository.AddAsync(relationTransaction);
            }

            item.TypeTransaction.AddToRoute(item, route);
            await _transationRepository.AddAsync(item);
            await _budgetaryFundRepository.UpdateAsync(route.Source);
            await _budgetaryFundRepository.UpdateAsync(route.Receiver);
            return item.RelationId;

        }

        //public async Task<bool> UpdateAsync(Transaction item, 
        //    BudgetaryFund sourceBF, BudgetaryFund receiverBF) =>
        //        await item.TypeTransaction.UpdateAsync(item, sourceBF, receiverBF, _transationRepository);

        //public async Task<bool> DeleteAsync(Transaction item,
        //    BudgetaryFund sourceBF, BudgetaryFund receiverBF) =>
        //        await item.TypeTransaction.DeleteAsync(item, sourceBF, receiverBF, _transationRepository);






        private readonly IRepository<BudgetaryFund> _budgetaryFundRepository;
        private readonly IRepository<Transaction> _transationRepository;

    }
}
