using System;
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


                Console.WriteLine(await Factory.Current.TypeTransactionRepository.AddAsync(t1Type));
                Console.WriteLine(await Factory.Current.TypeTransactionRepository.AddAsync(t2Type));

                foreach (ITypeTransaction tt in await Factory.Current.TypeTransactionRepository.GetAllAsync())
                    Console.WriteLine("{0} | {1} ", tt.Name, tt.GetType());

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
