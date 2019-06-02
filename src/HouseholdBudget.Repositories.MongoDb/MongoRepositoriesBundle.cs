using HouseholdBudget.Common.Entities;
using HouseholdBudget.DTO;
using MongoDB.Bson;
using MongoDB.Driver;


namespace HouseholdBudget.Repositories.MongoDb
{
    public class MongoRepositoriesBundle
    {
         
         public MongoRepositoriesBundle()
        {

            TransactionRepository = new MongoRepository<TransactionDTO>("Transactions");
            BudgetaryFundRepository = new MongoRepository<BudgetaryFundDTO>("BudgetaryFunds");
            TypeTransactionRepository = new MongoRepository<TypeTransactionDTO>("TypeTransaction");
        }

        public MongoRepository<TransactionDTO> TransactionRepository { get; }
        public MongoRepository<BudgetaryFundDTO> BudgetaryFundRepository { get; }

        public MongoRepository<TypeTransactionDTO> TypeTransactionRepository { get; }


    }
}
