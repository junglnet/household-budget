using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.DTO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HouseholdBudget.Repositories.MongoDb.EntityRepositories
{
    public class MongoTransactionRepository : IRepository<Transaction>
    {

        private readonly MongoRepositoriesBundle _bundle;
        private IRepository<ExpenseTypeTransaction> _expenceTypeTRepository;
        private IRepository<IncomeTypeTransaction> _incomeTypeTRepository;
        private IRepository<BalanceTypeTransaction> _balanceTypeTRepository;

        public MongoTransactionRepository (
            MongoRepositoriesBundle bundle,
            IRepository<ExpenseTypeTransaction> expenceTypeTRepository,
            IRepository<IncomeTypeTransaction> incomeTypeTRepository,
            IRepository<BalanceTypeTransaction> balanceTypeTRepository)
        {

            _bundle = bundle;
            _expenceTypeTRepository = expenceTypeTRepository;
            _incomeTypeTRepository = incomeTypeTRepository;
            _balanceTypeTRepository = balanceTypeTRepository;
            
        }

        public async Task<string> AddAsync(Transaction item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await _bundle.TransactionRepository.Collection.InsertOneAsync(item.ToDto());

            return item.Id;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var curItem = await GetByIdAsync(id);

            var deleteResult =
                await
                    _bundle.TransactionRepository.Collection.DeleteOneAsync(
                        new ExpressionFilterDefinition<TransactionDTO>(s => s.Id == id));

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);
        }

        public async Task<IReadOnlyList<Transaction>> GetAllAsync()
        {
            var asyncCursor = await _bundle.TransactionRepository.Collection
                .Find(FilterDefinition<TransactionDTO>.Empty).ToListAsync();

            var transactions = new List<Transaction>();

            foreach (TransactionDTO ts in asyncCursor)
            {
                transactions.Add(
                    await ts.ToTransaction(
                        _expenceTypeTRepository, 
                        _incomeTypeTRepository, 
                        _balanceTypeTRepository));
            }
            
            return transactions;
        }

        public async Task<Transaction> GetByIdAsync(string id) =>
            (await IsExistById(id)) ? (
                await (
                    await _bundle.TransactionRepository.Collection.Find(s => s.Id == id)
                    .FirstOrDefaultAsync())
                .ToTransaction(_expenceTypeTRepository, _incomeTypeTRepository, _balanceTypeTRepository)) 
            : null;
        

        public async Task<IReadOnlyList<Transaction>> GetByIdsAsync(string[] ids)
        {
            var funds = new List<Transaction>();
            foreach (string id in ids)
            {
                if (await IsExistById(id))

                    funds.Add(
                        await (
                            await _bundle.TransactionRepository.Collection.Find(s => s.Id == id).FirstOrDefaultAsync())
                        .ToTransaction(_expenceTypeTRepository, _incomeTypeTRepository, _balanceTypeTRepository));

            }

            return funds.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }

        public async Task<bool> IsExistById(string id) =>
            await _bundle.TransactionRepository.Collection
                .Find(s => s.Id == id).Limit(1).CountDocumentsAsync() != 0;

        public async Task<bool> UpdateAsync(Transaction item)
        {
            var replaceResult =
                await
                    _bundle.TransactionRepository.Collection.ReplaceOneAsync(
                        new ExpressionFilterDefinition<TransactionDTO>(s => s.Id == item.Id), item.ToDto());

            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }

        
    }
}
