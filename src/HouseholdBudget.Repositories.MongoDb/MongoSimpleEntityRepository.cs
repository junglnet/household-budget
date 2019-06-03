using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HouseholdBudget.Repositories.MongoDb.EntityRepositories
{
    public abstract class MongoSimpleEntityRepository<T> where T : EntityBase, new()
    {
        private readonly MongoRepository<T> _mongoRepository;


        public MongoSimpleEntityRepository(MongoRepository<T> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }


        public async Task<string> AddAsync(T item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await _mongoRepository.Collection.InsertOneAsync(item);
            return item.Id;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResult =
                 await
                    _mongoRepository.Collection.DeleteOneAsync(
                         new ExpressionFilterDefinition<T>(s => s.Id == id));

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() =>
            await _mongoRepository.Collection
                 .Find(FilterDefinition<T>.Empty)
                 .ToListAsync();


        public async Task<T> GetByIdAsync(string id) =>

         await _mongoRepository.Collection
               .Find(s => s.Id == id).FirstOrDefaultAsync();


        public async Task<IReadOnlyList<T>> GetByIdsAsync(string[] ids)
        {
            var lists = new List<T>();
            foreach (string id in ids)
            {
                if (await IsExistById(id))

                    lists.Add(await _mongoRepository.Collection
                    .Find(s => s.Id == id).FirstOrDefaultAsync());

            }

            return lists.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }

        public async Task<bool> IsExistById(string id) =>
            await _mongoRepository.Collection
                .Find(s => s.Id == id).Limit(1).CountDocumentsAsync() != 0;

        public async Task<bool> UpdateAsync(T item)
        {
            var replaceResult =
               await
                   _mongoRepository.Collection.ReplaceOneAsync(
                       new ExpressionFilterDefinition<T>(s => s.Id == item.Id), item);
            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }


    }

}
