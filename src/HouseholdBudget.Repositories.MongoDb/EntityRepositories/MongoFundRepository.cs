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
    public class MongoFundRepository : IRepository<Fund>
    {

        private readonly MongoRepositoriesBundle _bundle;
        private IRepository<Transaction> _transactionRepository;

        public MongoFundRepository(
            MongoRepositoriesBundle bundle,
           IRepository<Transaction> transactionRepository)
        {
            _bundle = bundle;
            _transactionRepository = transactionRepository;

        }


        public async Task<string> AddAsync(Fund item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();

            await AddOrUpdateTransactionAsync(item.Transactions);

            await _bundle.FundRepository.Collection.InsertOneAsync(item.ToDto());

            return item.Id;
        }


        public async Task<bool> DeleteAsync(string id)
        {

            var curItem = await GetByIdAsync(id);
            
            var deleteResult =
                await
                    _bundle.FundRepository.Collection.DeleteOneAsync(
                        new ExpressionFilterDefinition<FundDTO>(s => s.Id == id));

            await DeleteAllTransactionAsync(curItem.Transactions);

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);

        }


        public async Task<IReadOnlyList<Fund>> GetAllAsync()
        {
            var asyncCursor = await _bundle.FundRepository.Collection
                .Find(FilterDefinition<FundDTO>.Empty).ToListAsync();

            var funds = new List<Fund>();

            foreach (FundDTO bf in asyncCursor)            
                funds.Add(await bf.ToFund(_transactionRepository));            

            return funds;
        }


        public async Task<Fund> GetByIdAsync(string id) =>
            (await IsExistById(id)) ? (
                await (
                    await _bundle.FundRepository.Collection.Find(s => s.Id == id)
                    .FirstOrDefaultAsync())
                .ToFund(_transactionRepository)) 
            : null;
        

        public async Task<IReadOnlyList<Fund>> GetByIdsAsync(string[] ids)
        {
            var funds = new List<Fund>();
            foreach (string id in ids)
            {
                if (await IsExistById(id))

                    funds.Add(
                        await (
                            await _bundle.FundRepository.Collection.Find(s => s.Id == id).FirstOrDefaultAsync())
                        .ToFund(_transactionRepository));

            }

            return funds.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }


        public async Task<bool> UpdateAsync(Fund item)
        {
            await AddOrUpdateTransactionAsync(item.Transactions);

            var replaceResult =
                await
                    _bundle.FundRepository.Collection.ReplaceOneAsync(
                        new ExpressionFilterDefinition<FundDTO>(s => s.Id == item.Id), item.ToDto());

            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }


        public async Task<bool> IsExistById(string id) =>

            await _bundle.FundRepository.Collection
                .Find(s => s.Id == id).Limit(1).CountDocumentsAsync() != 0;

        private async Task AddOrUpdateTransactionAsync(List<Transaction> items)
        {
            if (items != null)
            {

                foreach (Transaction pp in items)
                {

                    if (pp.Id == null)
                        await _transactionRepository.AddAsync(pp);

                    else
                        await _transactionRepository.UpdateAsync(pp);
                }

            }

        }

        private async Task DeleteAllTransactionAsync(List<Transaction> items)
        {

            foreach (Transaction item in items)
                await _transactionRepository.DeleteAsync(item.Id);

        }

        
    }
}
