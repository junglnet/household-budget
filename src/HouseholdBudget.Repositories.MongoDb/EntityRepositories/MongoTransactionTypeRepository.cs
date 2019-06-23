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
    public class MongoTransactionTypeRepository : IRepository<TransactionType>
    {
        private readonly MongoRepositoriesBundle _bundle;
    
        public MongoTransactionTypeRepository(MongoRepositoriesBundle bundle)
        {

            _bundle = bundle;           

        }


        public async Task<string> AddAsync(TransactionType item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();                     

            await _bundle.TransactionTypeRepository.Collection.InsertOneAsync(item.ToDto());

            return item.Id;
        }


        public async Task<bool> DeleteAsync(string id)
        {

            var curItem = await GetByIdAsync(id);

            var deleteResult =
                await
                    _bundle.TransactionTypeRepository.Collection.DeleteOneAsync(
                        new ExpressionFilterDefinition<TransactionTypeDTO>(s => s.Id == id));                      

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);

        }


        public async Task<IReadOnlyList<TransactionType>> GetAllAsync()
        {
            var asyncCursor = await _bundle.TransactionTypeRepository.Collection
                .Find(FilterDefinition<TransactionTypeDTO>.Empty).ToListAsync();

            var transactionTypes = new List<TransactionType>();

            foreach (TransactionTypeDTO bf in asyncCursor)
                transactionTypes.Add(await bf.ToTransactionType(this));

            return transactionTypes;
        }


        public async Task<TransactionType> GetByIdAsync(string id) =>
            (await IsExistById(id)) ? (
                await (
                    await _bundle.TransactionTypeRepository.Collection.Find(s => s.Id == id)
                    .FirstOrDefaultAsync())
                .ToTransactionType(this))
            : null;


        public async Task<IReadOnlyList<TransactionType>> GetByIdsAsync(string[] ids)
        {
            var transactionTypes = new List<TransactionType>();
            foreach (string id in ids)
            {
                if (await IsExistById(id))

                    transactionTypes.Add(
                        await (
                            await _bundle.TransactionTypeRepository.Collection.Find(s => s.Id == id).FirstOrDefaultAsync())
                        .ToTransactionType(this));

            }

            return transactionTypes.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }


        public async Task<bool> UpdateAsync(TransactionType item)
        {
           
            var replaceResult =
                await
                    _bundle.TransactionTypeRepository.Collection.ReplaceOneAsync(
                        new ExpressionFilterDefinition<TransactionTypeDTO>(s => s.Id == item.Id), item.ToDto());

            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }


        public async Task<bool> IsExistById(string id) =>

            await _bundle.TransactionTypeRepository.Collection
                .Find(s => s.Id == id).Limit(1).CountDocumentsAsync() != 0;
              

    }
}
