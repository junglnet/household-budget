using HouseholdBudget.Common.Entities;
using HouseholdBudget.DTO;
using MongoDB.Bson;
using MongoDB.Driver;


namespace HouseholdBudget.Repositories.MongoDb
{
    public class MongoRepositoriesBundle
    {
         
         public MongoRepositoriesBundle(
             string connectionString,
             string databaseName)
        {

            TransactionRepository = new MongoRepository<TransactionDTO>("Transactions", connectionString, databaseName);
            FundRepository = new MongoRepository<FundDTO>("Funds", connectionString, databaseName);
            TransactionTypeRepository = new MongoRepository<TransactionTypeDTO>("TransactionType", connectionString, databaseName);
            TransactionRouteRepository = new MongoRepository<TransactionRouteDTO>("TransactionRoute", connectionString, databaseName);
        }

        public MongoRepository<TransactionDTO> TransactionRepository { get; }
        public MongoRepository<FundDTO> FundRepository { get; }
        public MongoRepository<TransactionRouteDTO> TransactionRouteRepository { get; set; }
        public MongoRepository<TransactionTypeDTO> TransactionTypeRepository { get; }
       

    }
}
