using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.DTO
{
    public class TransactionTypeDTO : DictionaryBase
    {
        public string ReverseTypeId { get; set; }
        public int TypeStatusId  { get;set;}

    }
}
