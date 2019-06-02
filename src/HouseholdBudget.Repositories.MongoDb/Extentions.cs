using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static async Task<Transaction> ToTransaction(this TransactionDTO dto,
           IRepository<ITypeTransaction> typeTransactionRepository) => new Transaction()
           {

               Id = dto.Id,
               Name = dto.Name,
               RelationId = dto.RelationId,
               PlannedSum = dto.PlannedSum,
               FactSum = dto.FactSum,
               TypeTransaction = await typeTransactionRepository.GetByIdAsync(dto.Id)

           };

        public static async Task<ITypeTransaction> ToTypeTransaction(this TypeTransactionDTO dto)
        {
                      
            if (dto.Type == typeof(BalanceTypeTransaction))
            {
                return new BalanceTypeTransaction()
                {
                    Id = dto.Id,
                    Name = dto.Name
                };    
            }

            if (dto.Type == typeof(ExpenseTypeTransaction))
            {
                return new ExpenseTypeTransaction()
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            }

            if (dto.Type == typeof(IncomeTypeTransaction))
            {
                return new IncomeTypeTransaction()
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            }

            else
                return null;

        }

    }
}
