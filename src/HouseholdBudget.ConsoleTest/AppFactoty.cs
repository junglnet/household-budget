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

            var typeTransactionRepository = new MongoTypeTransactionRepository(bundle);
            TypeTransactionRepository = typeTransactionRepository;

            var transactionRepository = new MongoTransactionRepository(bundle, TypeTransactionRepository);
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
        public override IRepository<ITypeTransaction> TypeTransactionRepository { get; }
        #endregion

    }
}
