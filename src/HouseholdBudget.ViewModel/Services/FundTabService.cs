using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common;
using HouseholdBudget.Common.Interfaces;

namespace HouseholdBudget.ViewModel.Services
{
    public class FundTabService
    {

        public async Task<List<FundTabListViewModel>> GetAllFundTabViewModel(IFundService fundService)
        {
            var fundList = await fundService.GetAllAsync().ConfigureAwait(false);      
            
            return await Task.Run(()=> fundList?.Select(f => new FundTabListViewModel(f)).ToList()).ConfigureAwait(false);           

        }

    }
}
