using System.Collections.Generic;
using System;
using HouseholdBudget.Common.Interfaces;

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
