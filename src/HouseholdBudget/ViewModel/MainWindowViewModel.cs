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
using HouseholdBudget.Entities;
using System.Windows.Input;
using System.Windows;
using HouseholdBudget.Interfaces;
using System.Threading;
using MvvmDialogs.ViewModels;
using HouseholdBudget.Services;

namespace HouseholdBudget.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IViewModel, ITabOwner
    {

        // TODO: Добавить команды управления фондами (добавить/удалить). Основной фонд должен жить.
        // TODO: Добавить поддержку material design.
        


        private FundTabViewModel currentTabViewModel;
        private ObservableCollection<IDialogViewModel> _dialogs;
        private DialogService _dialogService;


        public MainWindowViewModel()
        {

            AppFactoty.Init();

            CommandInit();

            Dialogs = new ObservableCollection<IDialogViewModel>();

            _dialogService = new DialogService(Dialogs);

            Tabs = new ObservableCollection<FundTabViewModel>();

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

        private async Task LoadFundAsync(CancellationToken token = default)
        {

            Tabs.Clear();

                var fundList = await Factory.Current.FundService.GetAllAsync();

                var fundViewmodelList = fundList.Select(obj => new FundTabViewModel(obj)).ToList();

                foreach (var item in fundViewmodelList)                
                    Tabs.Add(item);
            
        }

        
        private void CommandInit()
        {

              LoadAllFund = AsyncCommand.Create(async (token) => {

                  try
                  {


                      await LoadFundAsync(token);

                      
                  }
                  catch (Exception ex)
                  {

                      MessageBox.Show(ex.Message);
                      
                  }

              });
            
        }
          

        #region Properties
        public FundTabViewModel CurrentTabViewModel
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
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _dialogs; } set { _dialogs = value; } }

        public ObservableCollection<FundTabViewModel> Tabs { get; private set; }
        public ICommand CloseTabCommand { get; private set; }
        public ICommand LoadAllFund { get; private set; }
        #endregion

    }
}
