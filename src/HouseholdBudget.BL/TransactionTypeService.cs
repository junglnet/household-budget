using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.BL
{
    class TransactionTypeService : ITransactionTypeService
    {

        public TransactionTypeService(
            IRepository<TransactionType> transactionTypeRepository,
            ITransactionService transactionService)
        {

            _transactionTypeRepository = transactionTypeRepository;
            _transactionService = transactionService;

        }

        public async Task<IReadOnlyList<TransactionType>> GetAllAsync() =>
            await _transactionTypeRepository.GetAllAsync();

        public async Task<IReadOnlyList<TransactionType>> GetManyAsync(string[] ids) =>
            await _transactionTypeRepository.GetByIdsAsync(ids);

        public async Task<TransactionType> GetOneAsync(string id) =>
            await _transactionTypeRepository.GetByIdAsync(id);

        public async Task<bool> RemoveAsync(string id)
        {
            /*
             * 1. Получить список транзакций
             * 2. Если есть совпадения по ID вызвать исключение.
             * 3. Может быть ошибка
             */


            if ((await GetTransactionByTypeId(id)).Count > 0)
                throw new Exception("Тип используется в транзакциях. Удалить невозможно.");            

            return await _transactionTypeRepository.DeleteAsync(id);
        }

        public async Task<string> SaveAsync(TransactionType entity) =>
            await _transactionTypeRepository.AddAsync(entity);

        public async Task<bool> UpdateAsync(TransactionType entity) =>
            await _transactionTypeRepository.UpdateAsync(entity);

        private async Task<List<Transaction>> GetTransactionByTypeId(string transactionTypeId) =>
            (await _transactionService.GetAllAsync()).Where(t => t.TransactionType.Id == transactionTypeId).ToList();

        private readonly IRepository<TransactionType> _transactionTypeRepository;
        private readonly ITransactionService _transactionService;
    }
}
