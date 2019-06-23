using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.Common;
using HouseholdBudget.BL;
using HouseholdBudget.Repositories.MongoDb;
using HouseholdBudget.Repositories.MongoDb.EntityRepositories;

namespace HouseholdBudget.ViewModel
{
    public class AppFactoty : Factory
    {
        static AppFactoty()
        {
            Current = new AppFactoty();
        }

        private AppFactoty()
        {

            MongoRepositoriesBundle bundle = new MongoRepositoriesBundle("mongodb://localhost:27017", "householdbudget");

            var transctionTypeRepository = new MongoTransactionTypeRepository(bundle);
            TransctionTypeRepository = transctionTypeRepository;


            var transactionRepository = new MongoTransactionRepository(
                bundle,
                TransctionTypeRepository);
            TransactionRepository = transactionRepository;

            var fundRepository = new MongoFundRepository(bundle, TransactionRepository);
            FundRepository = fundRepository;

            var transactionRouteRepository = new MongoTransactionRouteRepository(
                bundle,
                TransactionRepository,
                FundRepository);

            TransactionRouteRepository = transactionRouteRepository;

            var transactionService = new TransactionService(TransactionRepository);
            TransactionService = transactionService;

            var fundService = new FundSevice(FundRepository, TransactionService);
            FundService = fundService;

            var transactionRouteEditService = new TransactionRouteService(transactionRouteRepository);
            TransactionRouteEditService = transactionRouteEditService;


        }

        internal static void Init()
        {
        }


        #region Overrides of Factory
        public override IRepository<Fund> FundRepository { get; }
        public override IRepository<Transaction> TransactionRepository { get; }
        public override IRepository<TransactionType> TransctionTypeRepository { get; }
        public override IRepository<TransactionRoute> TransactionRouteRepository { get; }
        public override ITransactionService TransactionService { get; }
        public override IFundService FundService { get; }
        public override ITransactionRouteService TransactionRouteEditService { get; }
        #endregion
    }
}
