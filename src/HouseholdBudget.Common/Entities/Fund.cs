using System.Collections.Generic;
using System;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.Common.Entities
{
    public class Fund : DictionaryBase
    {

        public Fund()
        {

            Transactions = new List<Transaction>();            
                
        }
        public void AddTransaction(Transaction item) =>
            Transactions.Add(item);
        
        public string Description { get; set; }


        public List<Transaction> Transactions { get; set; }

    }
}
