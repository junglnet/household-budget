using System.Collections.Generic;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.BL
{
    /// <summary>
    /// Класс описывает слой бизнес-логики транзакции
    /// </summary>
    public class TransactionService : ITransactionService
    {
        public TransactionService(
            IRepository<Transaction> transationRepository)

        {
            _transationRepository = transationRepository;
            
        }

        private readonly IRepository<Transaction> _transationRepository;

        public async Task<string> SaveAsync(Transaction entity) =>
            await _transationRepository.AddAsync(entity);

        public async Task<bool> UpdateAsync(Transaction entity) =>
            await _transationRepository.UpdateAsync(entity);

        public async Task<bool> RemoveAsync(string id) =>
            await _transationRepository.DeleteAsync(id);

        public async Task<Transaction> GetOneAsync(string id) =>
            await _transationRepository.GetByIdAsync(id);

        public async Task<IReadOnlyList<Transaction>> GetManyAsync(string[] ids) =>
            await _transationRepository.GetByIdsAsync(ids);

        public async Task<IReadOnlyList<Transaction>> GetAllAsync() =>
            await _transationRepository.GetAllAsync();
    }
}
