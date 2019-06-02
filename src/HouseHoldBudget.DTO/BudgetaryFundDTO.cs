using System.Collections.Generic;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.DTO
{
    public class BudgetaryFundDTO : DictionaryBase
    {
        public string Description { get; set; }
        public string[] TransactionsId { get; set; }

    }
}
