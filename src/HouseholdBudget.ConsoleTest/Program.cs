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
                
            BudgetaryFund mainFund = new BudgetaryFund("Основной бюджет");
            Transaction ts1 = new Transaction
            {
                Name = "Зачисление ЗП",
                PlannedSum = 30000,
                FactSum = 299800,
                TypeTransaction = new Income(),  
                TransactionSaver = new Income(),
                EndPointBudgetaryFund = mainFund
            };

            Transaction ts2 = new Transaction
            {
                Name = "Оплата телефона",                
                FactSum = 850000,                
                TypeTransaction = new Expense(),
                SourceBudgetaryFund = mainFund                
            };

            Transaction ts3 = new Transaction
            {
                Name = "Сальдо",
                FactSum = 10000,
                TypeTransaction = new Balance(),
                DateTime = new DateTime(2019, 05, 01),
                SourceBudgetaryFund = mainFund,
                EndPointBudgetaryFund = mainFund
            };

            Transaction ts4 = new Transaction
            {
                Name = "Оплата телефона",
                FactSum = 15000,
                DateTime = new DateTime(2019, 04, 10),
                TypeTransaction = new Expense(),
                SourceBudgetaryFund = mainFund
            };


            ts1.SaveTransaction();
            Transaction tsnew = (Transaction)ts1.Clone();

            Console.WriteLine("Операция {0}, план {1}, факт {2}, дата {3}, {4}",
                    ts1.Name, ts1.PlannedSum, ts1.FactSum, ts1.DateTime, ts1.Id);

            Console.WriteLine("Операция {0}, план {1}, факт {2}, дата {3}, {4}",
                    tsnew.Name, tsnew.PlannedSum, tsnew.FactSum, tsnew.DateTime, tsnew.Id);
            Console.ReadLine();
            Console.WriteLine(mainFund.AddTransaction(ts2));
            Console.WriteLine(mainFund.AddTransaction(ts3));
            Console.WriteLine(mainFund.AddTransaction(ts4));
           
            
            foreach (Transaction ts in mainFund.Transactions)
                Console.WriteLine("Операция {0}, план {1}, факт {2}, дата {3}", 
                    ts.Name, ts.PlannedSum, ts.FactSum, ts.DateTime);
            Console.WriteLine("...");

            foreach (Transaction ts in mainFund.GetTransactions(new DateTime(2019, 05, 01), new DateTime(2019, 05, 30)))
                Console.WriteLine("Операция {0}, план {1}, факт {2}, дата {3}",
                    ts.Name, ts.PlannedSum, ts.FactSum, ts.DateTime);

            Console.WriteLine(mainFund.GetTransactionsSum(new DateTime(2019, 05, 01), new DateTime(2019, 05, 30)));

            Console.WriteLine("Waiting any key...");
            Console.ReadLine();
        }
    }
}
