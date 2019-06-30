using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.BL;
using HouseholdBudget.ViewModel.Entities;
using System.Windows.Input;
using System.Windows;

namespace HouseholdBudget.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {

            AppFactoty.Init();
             
            FundTabsVM = new NotifyTaskCompletion<List<FundTabViewModel>>(GetAllFundTabViewModel());
            
        }

        
        private async Task<List<FundTabViewModel>> GetAllFundTabViewModel()
        {
            var fundList = await Factory.Current.FundService.GetAllAsync();
            return await Task.Run(() => fundList?.Select(f => new FundTabViewModel(f)).ToList());

        }
               
        private RelayCommand _closeTabCommand;
        public RelayCommand CloseTabCommand
        {
            get
            {
                return _closeTabCommand
                    ?? (_closeTabCommand = new RelayCommand(
                    (t) =>
                    {
                        FundTabsVM.Result.Remove((FundTabViewModel)t);
                        
                    }));
            }
        }

        private FundTabViewModel currentFundTabsVM;
        public FundTabViewModel CurrentFundTabsVM
        {
            get => currentFundTabsVM;           

            set =>
                OnPropertyChanged();
        }
        
        public NotifyTaskCompletion<List<FundTabViewModel>> FundTabsVM { get; private set; }              

      
                       
    }
}
