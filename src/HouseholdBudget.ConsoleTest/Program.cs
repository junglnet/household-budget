using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var b1 = new BudgetaryFund()
            {
                Name = "Test1"
            };

            var b2 = new BudgetaryFund()
            {
                Name = "Test1"
            };

            if (b1.Name != b1.Name ) Console.WriteLine("не равно");
            else Console.WriteLine("равно");

            Console.WriteLine("Waiting any key...");
            Console.ReadLine();
        }
    }
}
