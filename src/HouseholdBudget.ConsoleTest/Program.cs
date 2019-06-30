using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.Common;
using HouseholdBudget.BL;

namespace HouseholdBudget.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {

            try
            {
                AppFactoty.Init();
                var fund1 = new Fund()
                {
                    Name = "Fund 1 Name",
                    Transactions = new List<Transaction>(),
                    
                };

                var fund2 = new Fund()
                {
                    Name = "Fund 2 Name",
                    Transactions = new List<Transaction>(),

                };

                await Factory.Current.FundService.SaveAsync(fund1);
                await Factory.Current.FundService.SaveAsync(fund2);

            }

            catch (Exception ex)
            {

                WriteLine(ex.Message);
                WriteLine(ex.TargetSite);
               

            }

            WriteLine("Waiting any key...");
            ReadLine();
        }
    }
}
