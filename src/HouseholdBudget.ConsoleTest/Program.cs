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
                var tl = Factory.Current.TransactionService.GetAllAsync().GetAwaiter().GetResult();

                WriteLine(tl.Count);
                
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
