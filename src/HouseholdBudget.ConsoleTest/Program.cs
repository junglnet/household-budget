using System;
using System.Collections.Generic;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
                
            IBudgetaryFund mainFund = new BudgetaryFund("Основной бюджет");
            ITransaction ts1 = new Transaction
            {
                Name = "Зачисление ЗП",
                PlannedSum = 30000,
                FactSum = 299800,
                TypeTransaction = new Income(),               
                EndPointBudgetaryFund = mainFund
            };

            ITransaction ts2 = new Transaction
            {
                Name = "Оплата телефона",                
                FactSum = 850000,                
                TypeTransaction = new Expense(),
                SourceBudgetaryFund = mainFund                
            };

            ITransaction ts3 = new Transaction
            {
                Name = "Сальдо",
                FactSum = 10000,
                TypeTransaction = new Balance(),
                DateTime = new DateTime(2019, 05, 01),
                SourceBudgetaryFund = mainFund,
                EndPointBudgetaryFund = mainFund
            };

            ITransaction ts4 = new Transaction
            {
                Name = "Оплата телефона",
                FactSum = 15000,
                DateTime = new DateTime(2019, 04, 10),
                TypeTransaction = new Expense(),
                SourceBudgetaryFund = mainFund
            };

            

            mainFund.Transactions = new List<ITransaction>();
            mainFund.Transactions.Add(ts1);
            mainFund.Transactions.Add(ts2);
            mainFund.Transactions.Add(ts3);
            mainFund.Transactions.Add(ts4);
            mainFund.Transactions.Sort();

            foreach (ITransaction ts in mainFund.Transactions)
                Console.WriteLine("Операция {0}, план {1}, факт {2}, дата {3}", 
                    ts.Name, ts.PlannedSum, ts.FactSum, ts.DateTime);
            Console.WriteLine("...");

            foreach (ITransaction ts in mainFund.GetTransactions(new DateTime(2019, 05, 01), new DateTime(2019, 05, 30)))
                Console.WriteLine("Операция {0}, план {1}, факт {2}, дата {3}",
                    ts.Name, ts.PlannedSum, ts.FactSum, ts.DateTime);

            Console.WriteLine(mainFund.GetTransactionsSum(new DateTime(2019, 05, 01), new DateTime(2019, 05, 30)));

            Console.WriteLine("Waiting any key...");
            Console.ReadLine();
        }
    }
}
