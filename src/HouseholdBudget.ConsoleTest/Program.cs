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

                var t1TranType = new TransactionType()
                {
                    Name = "Перевод со счета",
                    TypeStatus = TypeStatuses.Expense                    

                };

                await Factory.Current.TransctionTypeRepository.AddAsync(t1TranType);
                var t2TranType = new TransactionType()
                {

                    Name = "Перевод на счет",
                    TypeStatus = TypeStatuses.Income,
                    ReverseType = t1TranType

                };

                await Factory.Current.TransctionTypeRepository.AddAsync(t2TranType);

                var fund1 = new Fund()
                {
                    Name = "Основной фонд",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            PlannedSum = 999,
                            FactSum = 999,
                            TransactionType = t1TranType,
                            Name = t1TranType.Name
                        }
                    }

                };

                var fund2 = new Fund()
                {
                    Name = "фонд",
                    

                };

                WriteLine("Сохранен фонд {0}", await Factory.Current.FundService.SaveAsync(fund1));
                WriteLine("Сохранен фонд {0}", await Factory.Current.FundService.SaveAsync(fund2));

                var t1Transaction = new Transaction()
                {

                    PlannedSum = 100,
                    FactSum = 110,
                    TransactionType = t2TranType                    

                };

                var fundEditOperationLayer = new FundEditOperationLayer(
                    Factory.Current.TransactionService, 
                    Factory.Current.FundService, 
                    Factory.Current.TransactionRouteEditService);

                ;

                var result = await fundEditOperationLayer.SaveTransactionToFundAsync(t1Transaction, fund1, fund2);

                WriteLine(result);
                // WriteLine(t1Transaction.Id);
                ReadLine();

                t1Transaction.PlannedSum = 777000;

                var гзвResult = await fundEditOperationLayer.UpdateTransactionInFundAsync(t1Transaction);

                WriteLine(гзвResult);
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
