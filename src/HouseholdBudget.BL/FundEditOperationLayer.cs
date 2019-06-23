using System;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.BL
{
    /// <summary>
    /// Слой бизнес-логики сохранения, изменения, удаления транзакции в фонде
    /// </summary>
    public class FundEditOperationLayer
    {

        public FundEditOperationLayer(
            ITransactionService transactionService, 
            IFundService fundService,
            ITransactionRouteService transactionRouteService)
        {
            _transactionService = transactionService;
            _fundService = fundService;
            _transactionRouteService = transactionRouteService;
            
        }

        /// <summary>
        /// Бизнес-логика сохранения транзакции в фонд
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sourceFund"></param>
        /// <param name="receiverFund"></param>
        /// <returns>Кортеж sourse transaction Id, receiver transaction Id</returns>
        public async Task<(string, string)> SaveTransactionToFundAsync(
            Transaction transaction,
            Fund sourceFund,
            Fund receiverFund)
        {

            if (sourceFund.Id == null)
                throw new Exception("Фонд-источник загружен некорректно.");

            (string, string) result = (null, null);

            var transactionRoute = new TransactionRoute();

            transaction.Name = transaction.TransactionType.Name;

            if (sourceFund.Id != receiverFund.Id && receiverFund != null)
            {
                if (receiverFund.Id == null)
                    throw new Exception("Фонд-получатель загружен некорректно.");

                if (transaction.GetReverseType() == null)
                    throw new Exception("Выбран тип транзакции без обратной транзакции.");
                

                var receiverTransaction = (Transaction)transaction.Clone();

                receiverTransaction.TransactionType = transaction.GetReverseType();

                receiverTransaction.Name = receiverTransaction.TransactionType.Name;

                transactionRoute.ReceiverFund = receiverFund;

                transactionRoute.ReceiverTransaction = receiverTransaction;
                    
                receiverFund.AddTransaction(receiverTransaction);

                result.Item2 = (await _transactionService.SaveAsync(receiverTransaction));

                await _fundService.UpdateAsync(receiverFund);

            }

            transactionRoute.SourceFund = sourceFund;

            transactionRoute.SourceTransaction = transaction;

            sourceFund.AddTransaction(transaction);

            result.Item1 = (await _transactionService.SaveAsync(transaction));

            await _fundService.UpdateAsync(sourceFund);

            await _transactionRouteService.SaveAsync(transactionRoute);

            return result;

        }

       
        /// <summary>
        /// Бизнес-логика удаления транзакции из фондов
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
       public async Task<bool> RemoveTransactionFromFundAsync(Transaction item)
        {
                       
            if (item.Id == null)
                return false;

            var transactionRoute = await _transactionRouteService.FindByTransactionId(item.Id);

            if (transactionRoute == null)
                return false;

            var SourceTransaction = 
                transactionRoute.SourceFund?.Transactions?.Find(t => t.Id == transactionRoute?.SourceTransaction?.Id);

            if (SourceTransaction != null)
            {

                SourceTransaction.IsMarkedToDelete = true;

                await _fundService.UpdateAsync(transactionRoute.SourceFund);

                await _transactionService.RemoveAsync(SourceTransaction.Id);

            }                

            var ReiceverTransaction =
               transactionRoute.ReceiverFund?.Transactions?.Find(t => t.Id == transactionRoute?.ReceiverTransaction?.Id);

            if (ReiceverTransaction != null)
            {

                ReiceverTransaction.IsMarkedToDelete = true;

                await _fundService.UpdateAsync(transactionRoute.ReceiverFund);

                await _transactionService.RemoveAsync(ReiceverTransaction.Id);

            }

            return await _transactionRouteService.RemoveAsync(transactionRoute.Id);
        }

        /// <summary>
        /// Бизнес-логика обновления транзакции в фонде
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTransactionInFundAsync(Transaction item)
        {

            if (item.Id == null)
                return false;

            var transactionRoute = await _transactionRouteService.FindByTransactionId(item.Id);

            if (transactionRoute == null)
                return false;

            var SourceTransaction =
                transactionRoute.SourceFund?.Transactions?.Find(t => t.Id == transactionRoute?.SourceTransaction?.Id);

            var ReiceverTransaction =
               transactionRoute.ReceiverFund?.Transactions?.Find(t => t.Id == transactionRoute?.ReceiverTransaction?.Id);
                        
            if (ReiceverTransaction != null)
            {

                ReiceverTransaction.DateTime = item.DateTime;
                ReiceverTransaction.FactSum = item.FactSum;
                ReiceverTransaction.PlannedSum = item.PlannedSum;

                if (ReiceverTransaction.Id == item.Id)
                {
                    ReiceverTransaction.Name = item.TransactionType.Name;
                    ReiceverTransaction.TransactionType = item.TransactionType;
                }
                
                await _transactionService.UpdateAsync(ReiceverTransaction);

            }

            SourceTransaction.DateTime = item.DateTime;
            SourceTransaction.FactSum = item.FactSum;
            SourceTransaction.PlannedSum = item.PlannedSum;

            if (SourceTransaction.Id == item.Id)
            {
                SourceTransaction.Name = item.TransactionType.Name;
                SourceTransaction.TransactionType = item.TransactionType;
            }

            return await _transactionService.UpdateAsync(SourceTransaction);
        }

        private readonly ITransactionService _transactionService;
        private readonly IFundService _fundService;
        private readonly ITransactionRouteService _transactionRouteService;       

    }
}
