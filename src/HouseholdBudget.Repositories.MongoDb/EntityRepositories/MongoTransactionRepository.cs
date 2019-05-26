using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HouseholdBudget.Repositories.MongoDb.EntityRepositories
{
    public class MongoTransactionRepository : IRepository<Transaction>
    {

        private readonly MongoRepositoriesBundle _bundle;

        public MongoTransactionRepository (
            MongoRepositoriesBundle bundle)
        {

            _bundle = bundle;
            
        }


        public async Task<string> AddAsync(Transaction item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await _bundle.TransactionRepository.Collection.InsertOneAsync(item);
            return item.Id;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResult =
                 await
                    _bundle.TransactionRepository.Collection.DeleteOneAsync(
                         new ExpressionFilterDefinition<Transaction>(s => s.Id == id));

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);
        }

        public async Task<IReadOnlyList<Transaction>> GetAllAsync() =>
            await _bundle.TransactionRepository.Collection
                 .Find(FilterDefinition<Transaction>.Empty)
                 .ToListAsync();

        public async Task<Transaction> GetByIdAsync(string id)
        {
            if (await IsExistById(id))

                return await _bundle.TransactionRepository.Collection
               .Find(s => s.Id == id).Limit(1).FirstAsync();

            else
            {
                return new Transaction();
            }
        }

        public async Task<IReadOnlyList<Transaction>> GetByIdsAsync(string[] ids)
        {
            var lists = new List<Transaction>();
            foreach (string id in ids)
            {
                if (await IsExistById(id))

                    lists.Add(await _bundle.TransactionRepository.Collection
                    .Find(s => s.Id == id).Limit(1).FirstAsync());

            }

            return lists.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }

        public async Task<bool> IsExistById(string id) =>
            await _bundle.TransactionRepository.Collection
                .Find(s => s.Id == id).Limit(1).CountDocumentsAsync() != 0;

        public async Task<bool> UpdateAsync(Transaction item)
        {
            var replaceResult =
               await
                   _bundle.TransactionRepository.Collection.ReplaceOneAsync(
                       new ExpressionFilterDefinition<Transaction>(s => s.Id == item.Id), item);
            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }
    }
}
