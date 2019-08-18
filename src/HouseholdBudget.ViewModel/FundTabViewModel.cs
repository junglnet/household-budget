using HouseholdBudget.Common.Entities;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HouseholdBudget.ViewModel.Entities;
using System.Collections.Generic;
using HouseholdBudget.ViewModel.Interfaces;
using HouseholdBudget.Common;
using System.Threading.Tasks;
using System.Threading;

namespace HouseholdBudget.ViewModel
{
    public sealed class FundTabListViewModel : ViewModelBase, ITabViewModel
    {
       // private ITabOwner _mainWindowViewModel;
        private Fund _currentFund;
        private Transaction _selectedItem;

        public FundTabListViewModel(Fund fund)
        {

            Header = fund.Name;

           // _mainWindowViewModel = mainWindowViewModel;

            CurrentFund = fund;

            LoadFundCommand = AsyncCommand.Create(token => LoadCurrentFundAsync(token));
        }

        private async Task<Fund> LoadCurrentFundAsync(CancellationToken token)
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

        public Transaction SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }

                }
        public string Header { get; private set; }
        public IAsyncCommand LoadFundCommand { get; private set; }
        public IAsyncCommand DeleteTransactionCommand { get; private set; }
        public IAsyncCommand OpenTransactionCommand { get; private set; }
        public IAsyncCommand AddTransactionCommand { get; private set; }
    }
}
