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
    public class MongoTypeTransactionRepository : IRepository<ITypeTransaction>
    {

        private readonly MongoRepositoriesBundle _bundle;       

        public MongoTypeTransactionRepository(
            MongoRepositoriesBundle bundle)
        {

            _bundle = bundle;
            
        }

        public async Task<string> AddAsync(ITypeTransaction item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await _bundle.TypeTransactionRepository.Collection.InsertOneAsync(item.ToDto());

            return item.Id;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var curItem = await GetByIdAsync(id);

            var deleteResult =
                await
                    _bundle.TypeTransactionRepository.Collection.DeleteOneAsync(
                        new ExpressionFilterDefinition<TypeTransactionDTO>(s => s.Id == id));

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);
        }

        public async Task<IReadOnlyList<ITypeTransaction>> GetAllAsync()
        {
            var asyncCursor = await _bundle.TypeTransactionRepository.Collection
                .Find(FilterDefinition<TypeTransactionDTO>.Empty).ToListAsync();

            var tmp = new List<ITypeTransaction>();

            foreach (TypeTransactionDTO ts in asyncCursor)
            {
                tmp.Add(await ts.ToTypeTransaction());
            }

            return tmp;
        }


        public async Task<ITypeTransaction> GetByIdAsync(string id)
        {
            if (await IsExistById(id))

                return await _bundle.TypeTransactionRepository.Collection
               .Find(s => s.Id == id).Limit(1).FirstOrDefault().ToTypeTransaction();

            else
                return new ExpenseTypeTransaction();
        }

        public async Task<IReadOnlyList<ITypeTransaction>> GetByIdsAsync(string[] ids)
        {
            var tmp = new List<ITypeTransaction>();
            foreach (string id in ids)
            {
                if (await IsExistById(id))

                    tmp.Add(await _bundle.TypeTransactionRepository.Collection
                    .Find(s => s.Id == id).Limit(1).FirstAsync().Result.ToTypeTransaction());

            }

            return tmp.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }

        public async Task<bool> IsExistById(string id) =>
            await _bundle.TypeTransactionRepository.Collection
                .Find(s => s.Id == id).Limit(1).CountDocumentsAsync() != 0;

        public async Task<bool> UpdateAsync(ITypeTransaction item)
        {
            var replaceResult =
                await
                    _bundle.TypeTransactionRepository.Collection.ReplaceOneAsync(
                        new ExpressionFilterDefinition<TypeTransactionDTO>(s => s.Id == item.Id), item.ToDto());

            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }
    }
}
