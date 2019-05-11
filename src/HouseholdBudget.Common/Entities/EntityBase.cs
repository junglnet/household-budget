
namespace HouseholdBudget.Common.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {

        }

        protected EntityBase(string id)
        {
            Id = id;
        }

        
        public string Id { get; set; }
    }
}
