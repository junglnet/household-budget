using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.Common;

namespace HouseholdBudget.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {

            try
            {
                AppFactoty.Init();

                var t1Type = new ExpenseTypeTransaction()
                {
                    Name = "Оплата квартплаты"
                };

                var t2Type = new IncomeTypeTransaction()
                {
                    Name = "Зачисление ЗП"
                };

                Factory.Current.ExpenseTransactionRepository.AddAsync(t1Type);
                Factory.Current.IncomeTransactionRepository.AddAsync(t2Type);

                var t1transac = new Transaction()
                {
                   PlannedSum = 33333,
                   FactSum = 4444,
                   TypeTransaction = t1Type,
                   Name = t1Type.Name

                };

                var t2transac = new Transaction()
                {
                    PlannedSum = 221,
                    FactSum = 4333444,
                    TypeTransaction = t2Type,
                    Name = t2Type.Name

                };

                var b1Fund = new BudgetaryFund()
                {
                    Name = "Main Fund",
                    Transactions = new List<Transaction>()
                    {
                        t1transac,
                        t2transac
                    }

                };

                var bid = await Factory.Current.BudgetaryFundRepository.AddAsync(b1Fund);
                var btmp = await Factory.Current.BudgetaryFundRepository.GetByIdAsync(bid);
                Console.ReadLine();
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Waiting any key...");
            Console.ReadLine();
        }
    }
}
