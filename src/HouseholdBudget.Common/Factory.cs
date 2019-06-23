using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common
{
    public abstract class Factory
    {
        public static Factory Current { get; protected set; }
        public abstract IRepository<Fund> FundRepository { get; }
        public abstract IRepository<Transaction> TransactionRepository { get; }
        public abstract IRepository<TransactionType> TransctionTypeRepository { get; }      
        public abstract IRepository<TransactionRoute> TransactionRouteRepository { get; }
        public abstract ITransactionService TransactionService { get; }
        public abstract IFundService FundService { get; }
        public abstract ITransactionRouteService TransactionRouteEditService { get; }
        
        

    }
}
