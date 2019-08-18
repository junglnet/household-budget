using HouseholdBudget.Common.Entities;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using HouseholdBudget.Common;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Windows;
using HouseholdBudget.Entities;
using HouseholdBudget.Interfaces;

namespace HouseholdBudget.ViewModel
{
    public sealed class FundTabViewModel : ViewModelBase, ITabViewModel
    {

         // TODO: Передача Dialogs или DialogsService.
        // TODO: Добавить logger.
        // TODO: Создать и использовать в сущночти фонда список транзакций с сальдо.

        private Fund _currentFund;
        private Transaction _selectedTransaction;
        public FundTabViewModel(Fund fund)
        {

            Header = fund.Name;

           // _mainWindowViewModel = mainWindowViewModel;

            CurrentFund = fund;

            CommandInit();
            
        }

        private void CommandInit()
        {

            LoadFundCommand = AsyncCommand.Create(async (token) =>
            {

                try
                {

                    await LoadCurrentFundAsync(token);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                

            });
            
        }

        private async Task<Fund> LoadCurrentFundAsync(CancellationToken token = default)
        {

            return await Factory.Current.FundService.GetOneAsync(_currentFund.Id);

        }

       
        public Fund CurrentFund
        {
            get
            {
                return _currentFund;
            }

            set
            {
                _currentFund = value;
                OnPropertyChanged();
            }
        }

        public Transaction SelectedTransaction
        {
            get
            {
                return _selectedTransaction;
            }
            set
            {
                _selectedTransaction = value;
                OnPropertyChanged();
            }

                }
        public string Header { get; private set; }
        public ICommand LoadFundCommand { get; private set; }
        public ICommand DeleteTransactionCommand { get; private set; }
        public ICommand OpenTransactionCommand { get; private set; }
        public ICommand AddTransactionCommand { get; private set; }
    }
}
