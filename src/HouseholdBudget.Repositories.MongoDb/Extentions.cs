using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.DTO;

namespace HouseholdBudget.Repositories.MongoDb
{
    public static class Extentions
    {
        public static async Task<BudgetaryFund> ToBudgetaryFund(this BudgetaryFundDTO dto,
            IRepository<Transaction> transactionsRepository) => new BudgetaryFund()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Transactions = (List<Transaction>)await transactionsRepository.GetByIdsAsync(dto.TransactionsId)

            };

        public static async Task<Transaction> ToTransaction(
            this TransactionDTO dto,
            IRepository<ExpenseTypeTransaction> expenseTypeTRepository,
            IRepository<IncomeTypeTransaction> incomeTypeTRepository,
            IRepository<BalanceTypeTransaction> balanceTypeTRepository) 
           {

            var tmpTransaction = new Transaction()
            {

                Id = dto.Id,
                Name = dto.Name,
                RelationId = dto.RelationId,
                PlannedSum = dto.PlannedSum,
                FactSum = dto.FactSum

            };

            if (await expenseTypeTRepository.IsExistById(dto.TypeTransactionId))
            {
                tmpTransaction.TypeTransaction = await expenseTypeTRepository.GetByIdAsync(dto.TypeTransactionId);
                return tmpTransaction;
            }
                

            if (await incomeTypeTRepository.IsExistById(dto.TypeTransactionId))
            {
                tmpTransaction.TypeTransaction = await incomeTypeTRepository.GetByIdAsync(dto.TypeTransactionId);
                return tmpTransaction;
            }
                

            if (await balanceTypeTRepository.IsExistById(dto.TypeTransactionId))
            {
                tmpTransaction.TypeTransaction = await balanceTypeTRepository.GetByIdAsync(dto.TypeTransactionId);
                return tmpTransaction;
            }
                

            else
                tmpTransaction.TypeTransaction = null;

            return tmpTransaction;


        }        

    }
}
