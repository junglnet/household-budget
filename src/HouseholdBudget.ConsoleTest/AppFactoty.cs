using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.Common;
using HouseholdBudget.Repositories.MongoDb;
using HouseholdBudget.Repositories.MongoDb.EntityRepositories;

namespace HouseholdBudget.ConsoleTest
{
    public class AppFactoty : Factory
    {
        static AppFactoty()
        {
            Current = new AppFactoty();
        }

        private AppFactoty()
        {

            MongoRepositoriesBundle bundle = new MongoRepositoriesBundle();

            var expenseTransactionRepository = new MongoExpenseTypeTRepository(bundle);
            ExpenseTransactionRepository = expenseTransactionRepository;

            var incomeTransactionRepository = new MongoIncomeTypeTRepository(bundle);
            IncomeTransactionRepository = incomeTransactionRepository;

            var balanceTransactionRepository = new MongoBalanceTypeTRepository(bundle);
            BalanceTransactionRepository = balanceTransactionRepository;

            var transactionRepository = new MongoTransactionRepository(
                bundle, 
                ExpenseTransactionRepository, 
                IncomeTransactionRepository, 
                BalanceTransactionRepository);
            TransactionRepository = transactionRepository;

            var budgetaryFundRepository = new MongoBudgetaryFundRepository(bundle, TransactionRepository);
            BudgetaryFundRepository = budgetaryFundRepository;
        }

        internal static void Init()
        {
        }

        #region Overrides of Factory
        public override IRepository<BudgetaryFund> BudgetaryFundRepository { get; }
        public override IRepository<Transaction> TransactionRepository { get; }
        public override IRepository<ExpenseTypeTransaction> ExpenseTransactionRepository { get; }
        public override IRepository<IncomeTypeTransaction> IncomeTransactionRepository { get; }
        public override IRepository<BalanceTypeTransaction> BalanceTransactionRepository { get; }
        #endregion

    }
}
