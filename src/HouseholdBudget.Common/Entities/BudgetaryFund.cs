using System.Collections.Generic;
using System;

namespace HouseholdBudget.Common.Entities
{
    public class BudgetaryFund : DictionaryBase
    {

        public BudgetaryFund()
        {

            Transactions = new List<Transaction>();            
                
        }

        
        public string Description { get; set; }


        public List<Transaction> Transactions { get; set; }

    }
}
