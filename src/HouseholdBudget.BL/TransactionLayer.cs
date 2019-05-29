using System;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.BL
{
    /// <summary>
    /// Класс описывает слой бизнес-логики транзакции
    /// </summary>
    public class TransactionLayer
    {
        public TransactionLayer(
            IRepository<Transaction> transationRepository, 
            IRepository<BudgetaryFund> budgetaryFundRepository)

        {
            _transationRepository = transationRepository;
            _budgetaryFundRepository = budgetaryFundRepository;
        }

        public async Task<string> AddAsync(Transaction item, TransactionRoute route)

        {
            item.RelationId = Guid.NewGuid().ToString("N");

            if (!route.IsEqual())
            {
                var reverseTransation = (Transaction)item.Clone();
                reverseTransation.TypeTransaction = item.GetReverseType();
                route.Receiver.Transactions.Add(reverseTransation);
                await _budgetaryFundRepository.UpdateAsync(route.Receiver);
            }
            

            route.Source.Transactions.Add(item);
            
            await _budgetaryFundRepository.UpdateAsync(route.Source);

            return item.RelationId;

        }

        public async Task<bool> UpdateAsync(Transaction item, TransactionRoute route)
        {

            if (!route.IsEqual())
            {

                var tmpTransaction = route.Receiver.Transactions.Find(t => t.RelationId == item.RelationId);

                if (route.Receiver.Transactions.Remove(route.Receiver.Transactions.Find(t => t.RelationId == item.RelationId)))
                {

                    var reverseTransation = (Transaction)item.Clone();

                    reverseTransation.Id = tmpTransaction.Id;

                    reverseTransation.TypeTransaction = item.GetReverseType();

                    route.Receiver.Transactions.Add(reverseTransation);

                    await _transationRepository.UpdateAsync(reverseTransation);

                    await _budgetaryFundRepository.UpdateAsync(route.Receiver);
                }

                else
                    return false;

            }

            if (route.Source.Transactions.Remove(route.Source.Transactions.Find(t => t.RelationId == item.RelationId)))
            {

                route.Source.Transactions.Add(item);


                return await _transationRepository.UpdateAsync(item) &&
                    await _budgetaryFundRepository.UpdateAsync(route.Source) ?
                    true : false;
            }

            else
                return false;
                                 
                
        }
                
        public async Task<bool> DeleteAsync(Transaction item, TransactionRoute route)
        {

            if (!route.IsEqual())
            {

                var tmpTransaction = route.Receiver.Transactions.Find(t => t.RelationId == item.RelationId);

                if (route.Receiver.Transactions.Remove(route.Receiver.Transactions.Find(t => t.RelationId == item.RelationId)))
                {

                    await _transationRepository.DeleteAsync(tmpTransaction.Id);

                    await _budgetaryFundRepository.UpdateAsync(route.Receiver);
                }

                else
                    return false;
            }

            if (route.Source.Transactions.Remove(route.Source.Transactions.Find(t => t.RelationId == item.RelationId)))
            {

                await _budgetaryFundRepository.UpdateAsync(route.Source);

                return await _transationRepository.DeleteAsync(item.Id);

            }

            else
                return false;



        }


        private readonly IRepository<BudgetaryFund> _budgetaryFundRepository;
        private readonly IRepository<Transaction> _transationRepository;

    }
}
