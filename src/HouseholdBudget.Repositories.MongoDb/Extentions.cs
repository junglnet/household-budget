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
                        
            var tmpType = Type.GetType(dto.Type);
            ////Console.WriteLine(tmpType);
            //var instance = Activator.CreateInstance(tmpType);

            //FieldInfo idField = tmpType.GetField("Id");
            //FieldInfo nameField = tmpType.GetField("Name");

            //idField.SetValue(instance, dto.Id);
            //nameField.SetValue(instance, dto.Name);

            //return (ITypeTransaction)instance;


            if (tmpType == typeof(BalanceTypeTransaction))
            {
                return new BalanceTypeTransaction()
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            }

            if (tmpType == typeof(ExpenseTypeTransaction))
            {
                return new ExpenseTypeTransaction()
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            }

            if (tmpType == typeof(IncomeTypeTransaction))
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
