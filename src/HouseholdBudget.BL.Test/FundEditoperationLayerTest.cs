using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseholdBudget.BL;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common;

namespace HouseholdBudget.BL.Test
{
    [TestClass]
    public class FundEditoperationLayerTest
    {
        [TestMethod]
        public async Task SaveTransactionToFundTest()
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

            var fund0 = new Fund()
            {
                Name = "тест фонд",
                Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            PlannedSum = 200,
                            FactSum = 210,
                            TransactionType = t1TranType,
                            Name = t1TranType.Name
                        }
                    }

            };

            var fund1 = new Fund()
            {
                Name = "Основной фонд",
                Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            PlannedSum = 200,
                            FactSum = 210,
                            TransactionType = t1TranType,
                            Name = t1TranType.Name
                        }
                    }

            };

            var fund2 = new Fund()
            {
                Name = "фонд",


            };

            await Factory.Current.FundService.SaveAsync(fund1);
            await Factory.Current.FundService.SaveAsync(fund2);

            var t1Transaction = new Transaction()
            {

                PlannedSum = 100,
                FactSum = 110,
                TransactionType = t1TranType,
                Name = t1TranType.Name

            };

            var t2Transaction = new Transaction()
            {

                PlannedSum = 100,
                FactSum = 110,
                TransactionType = t2TranType,
                Name = t2TranType.Name

            };

            var fundEditOperationLayer = new FundEditOperationLayer(
                Factory.Current.TransactionService,
                Factory.Current.FundService,
                Factory.Current.TransactionRouteEditService);


            var result1 = await fundEditOperationLayer.SaveTransactionToFundAsync(t1Transaction, fund1, fund1);

            Assert.IsNotNull(result1.Item1);
            Assert.IsNull(result1.Item2);
           
            Assert.ThrowsException<Exception>(
                () => fundEditOperationLayer.SaveTransactionToFundAsync(
                    t1Transaction, fund1, fund2).GetAwaiter().GetResult());

            Assert.ThrowsException<Exception>(
                () => fundEditOperationLayer.SaveTransactionToFundAsync(
                    t1Transaction, fund0, fund0).GetAwaiter().GetResult());


            var result2 = await fundEditOperationLayer.SaveTransactionToFundAsync(t2Transaction, fund1, fund2);
            Assert.IsNotNull(result2.Item1);
            Assert.IsNotNull(result2.Item2);
        }
    }
}
