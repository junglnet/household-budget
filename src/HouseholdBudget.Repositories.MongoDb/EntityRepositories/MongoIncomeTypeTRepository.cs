using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Repositories.MongoDb.EntityRepositories
{
    public class MongoIncomeTypeTRepository :
        MongoSimpleEntityRepository<IncomeTypeTransaction>,
        IRepository<IncomeTypeTransaction>
    {

        public MongoIncomeTypeTRepository(MongoRepositoriesBundle bundle)
            : base(bundle.IncomeTypeTRepository)
        {
        }

    }
}
