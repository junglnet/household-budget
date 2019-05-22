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
    /// Класс описывает слой бизнес-логики бюджетного фонда 
    /// </summary>
    public class BudgetaryFundLayer
    {
        public BudgetaryFundLayer(IRepository<BudgetaryFund> budgetaryFundRepository)
        {
            _budgetaryFundRepository = budgetaryFundRepository;
        }

        /// <summary>
        /// Бмзнес-логика добавления нового бюджетного фонда
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<string> SaveAsync(BudgetaryFund item) 
        {
            return await _budgetaryFundRepository.AddAsync(item);
        }

        /// <summary>
        /// Бизнес-логика обновления бюджетного фонда
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(BudgetaryFund item)
        {
            return await _budgetaryFundRepository.UpdateAsync(item);
        }

        /// <summary>
        /// Бизнес-логика удаления бюджетного фонда
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            return await _budgetaryFundRepository.DeleteAsync(id);
        }

        private readonly IRepository<BudgetaryFund> _budgetaryFundRepository;
    }
}
