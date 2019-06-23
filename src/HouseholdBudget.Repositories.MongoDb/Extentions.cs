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
        public static async Task<Fund> ToFund(
            this FundDTO dto,
            IRepository<Transaction> transactionsRepository) => new Fund()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Transactions = (List<Transaction>)await transactionsRepository.GetByIdsAsync(dto.TransactionsId)

            };

        public static async Task<Transaction> ToTransaction(
            this TransactionDTO dto,
            IRepository<TransactionType> transactionTypeRepository) => new Transaction()
            {

                Id = dto.Id,
                Name = dto.Name,
                DateTime = dto.DateTime,
                PlannedSum = dto.PlannedSum,
                FactSum = dto.FactSum,                
                TransactionType = await transactionTypeRepository.GetByIdAsync(dto.TransactionTypeId)

            };
       

       

        public static async Task<TransactionRoute> ToTransactionRoute(
            this TransactionRouteDTO dto,
            IRepository<Transaction> transactionsRepository,
            IRepository<Fund> fundRepositiry) => new TransactionRoute()
            {
                Id = dto.Id,
                SourceTransaction = await transactionsRepository.GetByIdAsync(dto.SourceTransactionId),
                ReceiverTransaction = await transactionsRepository.GetByIdAsync(dto.ReceiverTransactionId),
                SourceFund = await fundRepositiry.GetByIdAsync(dto.SourceFundId),
                ReceiverFund = await  fundRepositiry.GetByIdAsync(dto.ReceiverFundId)
            };

        public static async Task<TransactionType> ToTransactionType(
            this TransactionTypeDTO dto, 
            IRepository<TransactionType> transactionTypeRepository) => new TransactionType()
        {
            Id = dto.Id,
            Name = dto.Name,
            ReverseType = await transactionTypeRepository.GetByIdAsync(dto.ReverseTypeId),
            TypeStatus = (TypeStatuses)Enum.GetValues(typeof(TypeStatuses)).GetValue(dto.TypeStatusId)

        };

    }
}
