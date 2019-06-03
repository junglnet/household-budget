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
            ExpenseTypeTRepository = new MongoRepository<ExpenseTypeTransaction>("ExpenseTypeTransaction");
            IncomeTypeTRepository = new MongoRepository<IncomeTypeTransaction>("IncomeTypeTransaction");
            BalanceTypeTRepository = new MongoRepository<BalanceTypeTransaction>("BalanceTypeTransaction");
        }

        public MongoRepository<TransactionDTO> TransactionRepository { get; }
        public MongoRepository<BudgetaryFundDTO> BudgetaryFundRepository { get; }
        public MongoRepository<ExpenseTypeTransaction> ExpenseTypeTRepository { get; }
        public MongoRepository<IncomeTypeTransaction> IncomeTypeTRepository { get; }
        public MongoRepository<BalanceTypeTransaction> BalanceTypeTRepository { get; }

    }
}
