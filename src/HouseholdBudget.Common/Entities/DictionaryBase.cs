namespace HouseholdBudget.Common.Entities
{
    public class DictionaryBase : EntityBase
    {
        public DictionaryBase()
        {

        }

        public DictionaryBase(string id, string name) : base(id)
        {
            this.Name = name;
        }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
    }
}
