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

        public async Task<string> AddAsync(Transaction item, BudgetaryFund budgetaryFund)

        {
                       
            budgetaryFund.Transactions.Add(item);
            var isUpdated = _transationRepository.AddAsync(item);
            await _budgetaryFundRepository.UpdateAsync(budgetaryFund);

            return isUpdated.Result;

        }

        public async Task<bool> UpdateAsync(Transaction item, BudgetaryFund budgetaryFund)
        {
            budgetaryFund.Transactions.Remove(budgetaryFund.Transactions.Find(t => t.Id == item.Id));
            budgetaryFund.Transactions.Add(item);

            var isUpdated = _transationRepository.UpdateAsync(item);

            await _budgetaryFundRepository.UpdateAsync(budgetaryFund);

            return isUpdated.Result;

        }
                



        private readonly IRepository<BudgetaryFund> _budgetaryFundRepository;
        private readonly IRepository<Transaction> _transationRepository;

    }
}
