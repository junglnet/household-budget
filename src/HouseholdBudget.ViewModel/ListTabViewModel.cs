using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudget.Common.Interfaces;
using HouseholdBudget.ViewModel.Entities;
using HouseholdBudget.ViewModel.Interfaces;
using HouseholdBudget.Common.Entities;
using HouseholdBudget.Common;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using System.Windows;

namespace HouseholdBudget.ViewModel
{
    /// <summary>
    /// Класс описывает вкладку со списком
    /// </summary>    
    public abstract class ListTabViewModel<T> : ViewModelBase, ITabViewModel where T : DictionaryBase
    {

        protected ITabOwner _mainWindowViewModel;
        protected ListTabViewModel(ITabOwner mainWindowViewModel)
        {

            _mainWindowViewModel = mainWindowViewModel;

            LoadAllCommand = AsyncCommand.Create(token => LoadAllAsync(token));

            AddItemCommand = new RelayCommand(obj => AddItem());

            OpenItemCommand = new RelayCommand(obj => OpenItem(obj as T));

            DeleteItemCommand = new RelayCommand(obj => DeleteItemAsync(obj as T));


        }

        protected abstract Task<IReadOnlyList<T>> LoadAllAsync(CancellationToken token);
        protected abstract void DeleteItemAsync(T item);
        protected abstract void OpenItem(T item);
        protected abstract void AddItem();


        public string Header { get; protected set; }
        public IAsyncCommand LoadAllCommand { get; private set; }
        public ICommand DeleteItemCommand { get; private set; }
        public ICommand OpenItemCommand { get; private set; }
        public ICommand AddItemCommand { get; private set; }

    }
}
