using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Repositories.MongoDb.EntityRepositories
{
    public class MongoBalanceTypeTRepository :
        MongoSimpleEntityRepository<BalanceTypeTransaction>,
        IRepository<BalanceTypeTransaction>
    {

        public MongoBalanceTypeTRepository(MongoRepositoriesBundle bundle)
            : base(bundle.BalanceTypeTRepository)
        {
        }

    }
}
