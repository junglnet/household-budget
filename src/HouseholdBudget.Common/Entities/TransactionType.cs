
namespace HouseholdBudget.Common.Entities
{
    public class TransactionType : DictionaryBase
    {
        public TransactionType ReverseType { get; set; }

        public TypeStatuses TypeStatus { get; set; }

    }

    public enum TypeStatuses
    {
        /// <summary>
        /// Сальдо
        /// </summary>
        Balance = 0,

        /// <summary>
        /// Расходная операция
        /// </summary>
        Expense = 1,

        /// <summary>
        /// Доходная операция
        /// </summary>
        Income = 2
    }
}
