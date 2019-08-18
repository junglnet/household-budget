using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.BL
{
    /// <summary>
    /// Класс описывает слой бизнес-логики бюджетного фонда 
    /// </summary>
    public class FundSevice : IFundService
    {
        public FundSevice(
            IRepository<Fund> fundRepository,
            ITransactionService transactionService)
        {
            _fundRepository = fundRepository;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Бмзнес-логика добавления нового бюджетного фонда
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<string> SaveAsync(Fund item) 
        {
            return await _fundRepository.AddAsync(item);
        }

        /// <summary>
        /// Бизнес-логика обновления бюджетного фонда
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Fund item)
        {

            
            await RemoveTransactionAsync(item.Transactions.Where(t => t.IsMarkedToDelete == true).ToList());

            item.Transactions.RemoveAll(t => t.IsMarkedToDelete == true);           

            return await _fundRepository.UpdateAsync(item);
        }

        /// <summary>
        /// Бизнес-логика удаления бюджетного фонда
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string id)
        {

            var fundToDelete = await _fundRepository.GetByIdAsync(id);
            
            await RemoveTransactionAsync(fundToDelete.Transactions);

            return await _fundRepository.DeleteAsync(id);
            
            
        }

        private async Task RemoveTransactionAsync(List<Transaction> transactionsToDelete)
        {
            if (transactionsToDelete != null)
                foreach (Transaction item in transactionsToDelete)
                    await _transactionService.RemoveAsync(item.Id);
            
        }

        public async Task<Fund> GetOneAsync(string id)
        {
            return await _fundRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Fund>> GetManyAsync(string[] ids)
        {
            return await _fundRepository.GetByIdsAsync(ids);
        }

        public async Task<IReadOnlyList<Fund>> GetAllAsync()
        {
            return await _fundRepository.GetAllAsync().ConfigureAwait(false);
        }

        private readonly IRepository<Fund> _fundRepository;
        private readonly ITransactionService _transactionService;
    }
}
