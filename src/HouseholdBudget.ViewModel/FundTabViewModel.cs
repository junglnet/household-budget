using HouseholdBudget.Common.Entities;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HouseholdBudget.ViewModel.Entities;
using System.Collections.Generic;

namespace HouseholdBudget.ViewModel
{
    public class FundTabViewModel : ViewModelBase
    {

        public FundTabViewModel(Fund fund)
        {

            Fund = fund;
            TabName = fund.Name;
            
        }

        public NotifyTaskCompletion<IReadOnlyList<Fund>> Fundtmp { get; set; }
        public string TabName {get; }
        public Fund Fund { get; }
        public ICommand AddTransaction { get;  }
        public ICommand RemoveTransaction { get; }
        public ICommand Reload { get; }



    }
}
