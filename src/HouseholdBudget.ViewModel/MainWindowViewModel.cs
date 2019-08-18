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
using HouseholdBudget.ViewModel.Services;
using HouseholdBudget.ViewModel.Interfaces;

namespace HouseholdBudget.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private FundTabListViewModel currentTabViewModel;
        public MainWindowViewModel()
        {

            AppFactoty.Init();
            CommandInit();
            Tabs = new ObservableCollection<FundTabListViewModel>();
            OnLoad();


        }

        public async void OnLoad()
        {
            try
            {
                await LoadFundAsync();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private async Task LoadFundAsync()
        {
            Tabs.Clear();
            try
            {                
                var r1 = await Factory.Current.FundService.GetAllAsync();
                var r2 = r1.Select(t => new FundTabListViewModel(t)).ToList();
                foreach (var t in r2)
                {
                    Tabs.Add(t);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

           

        }

        
        private void CommandInit()
        {

              LoadAllFund = AsyncCommand.Create(token => LoadFundAsync());
          //  LoadAllFund = new RelayCommand(f=> LoadFundAsync());
            
        }

        #region Command methods
        

        #endregion

        #region Properties
        public FundTabListViewModel CurrentTabViewModel
        {
            get
            {
                return currentTabViewModel;
            }

            set
            {
                currentTabViewModel = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<FundTabListViewModel> Tabs { get; private set; }
        public ICommand CloseTabCommand { get; private set; }
        public ICommand LoadAllFund { get; private set; }
        #endregion

    }
}
