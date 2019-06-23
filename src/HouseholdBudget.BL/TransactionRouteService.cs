using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.BL
{
    public class TransactionRouteService : ITransactionRouteService
    {
        public TransactionRouteService(           
            IRepository<TransactionRoute> transactionRouteRepository)
        {

            _transactionRouteRepository = transactionRouteRepository;

        }
        
        public async Task<TransactionRoute> FindByTransactionId(string id) =>
            (await _transactionRouteRepository.GetAllAsync())?
            .Where(t => t.ReceiverTransaction?.Id == id || t.SourceTransaction?.Id == id)
            .FirstOrDefault();
                   
                
       

        public async Task<string> SaveAsync(TransactionRoute entity) =>
            await _transactionRouteRepository.AddAsync(entity);

        public async Task<bool> UpdateAsync(TransactionRoute entity) =>
            await _transactionRouteRepository.UpdateAsync(entity);

        public async Task<bool> RemoveAsync(string id) =>
            await _transactionRouteRepository.DeleteAsync(id);

        private readonly IRepository<TransactionRoute> _transactionRouteRepository;
       
    }
}
