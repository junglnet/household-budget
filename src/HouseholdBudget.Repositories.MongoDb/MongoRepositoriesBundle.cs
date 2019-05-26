using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;


namespace HouseholdBudget.Repositories.MongoDb
{
    public class MongoRepositoriesBundle
    {
         
         public MongoRepositoriesBundle()
        {

            TransactionRepository = new MongoRepository<Transaction>("Transactions");
            BudgetaryFundRepository = new MongoRepository<BudgetaryFund>("BudgetaryFunds");
        }

        public MongoRepository<Transaction> TransactionRepository { get; }
        public MongoRepository<BudgetaryFund> BudgetaryFundRepository { get; }


    }
}
