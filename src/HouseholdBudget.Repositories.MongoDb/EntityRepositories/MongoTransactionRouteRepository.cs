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
    public class MongoTransactionRouteRepository : IRepository<TransactionRoute>
    {

        private readonly MongoRepositoriesBundle _bundle;
        private IRepository<Transaction> _transactionRepository;
        private IRepository<Fund> _fundRepository;

        public MongoTransactionRouteRepository(
            MongoRepositoriesBundle bundle,
           IRepository<Transaction> transactionRepository,
           IRepository<Fund> fundRepository)
        {
            _bundle = bundle;
            _transactionRepository = transactionRepository;
            _fundRepository = fundRepository;

        }


        public async Task<string> AddAsync(TransactionRoute item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();

            await _bundle.TransactionRouteRepository.Collection.InsertOneAsync(item.ToDto());

            return item.Id;
        }


        public async Task<bool> DeleteAsync(string id)
        {

            var curItem = await GetByIdAsync(id);

            var deleteResult =
                await
                    _bundle.TransactionRouteRepository.Collection.DeleteOneAsync(
                        new ExpressionFilterDefinition<TransactionRouteDTO>(s => s.Id == id));

            

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);

        }


        public async Task<IReadOnlyList<TransactionRoute>> GetAllAsync()
        {
            var asyncCursor = await _bundle.TransactionRouteRepository.Collection
                .Find(FilterDefinition<TransactionRouteDTO>.Empty).ToListAsync();

            var transactionRoutes = new List<TransactionRoute>();

            foreach (TransactionRouteDTO tr in asyncCursor)
                transactionRoutes.Add(await tr.ToTransactionRoute(_transactionRepository, _fundRepository));

            return transactionRoutes;
        }


        public async Task<TransactionRoute> GetByIdAsync(string id) =>
            (await IsExistById(id)) ? (
                await (
                    await _bundle.TransactionRouteRepository.Collection.Find(s => s.Id == id)
                    .FirstOrDefaultAsync())
                .ToTransactionRoute(_transactionRepository, _fundRepository))
            : null;


        public async Task<IReadOnlyList<TransactionRoute>> GetByIdsAsync(string[] ids)
        {
            var transactionRoutes = new List<TransactionRoute>();
            foreach (string id in ids)
            {
                if (await IsExistById(id))

                    transactionRoutes.Add(
                        await (
                            await _bundle.TransactionRouteRepository.Collection.Find(s => s.Id == id).FirstOrDefaultAsync())
                        .ToTransactionRoute(_transactionRepository, _fundRepository));

            }

            return transactionRoutes.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }


        public async Task<bool> UpdateAsync(TransactionRoute item)
        {
            
            var replaceResult =
                await
                    _bundle.TransactionRouteRepository.Collection.ReplaceOneAsync(
                        new ExpressionFilterDefinition<TransactionRouteDTO>(s => s.Id == item.Id), item.ToDto());

            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }


        public async Task<bool> IsExistById(string id) =>

            await _bundle.TransactionRouteRepository.Collection
                .Find(s => s.Id == id).Limit(1).CountDocumentsAsync() != 0;

    }
}
