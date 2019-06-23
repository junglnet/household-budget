using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.DTO
{
    public class FundDTO : DictionaryBase
    {
        public string Description { get; set; }
        public string[] TransactionsId { get; set; }

    }
}
