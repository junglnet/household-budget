using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Repositories.MongoDb.EntityRepositories
{
    public class MongoExpenseTypeTRepository : 
        MongoSimpleEntityRepository<ExpenseTypeTransaction>, 
        IRepository<ExpenseTypeTransaction>
    {        

        public MongoExpenseTypeTRepository(MongoRepositoriesBundle bundle)
            : base(bundle.ExpenseTypeTRepository)
        {
        }

    }
}
