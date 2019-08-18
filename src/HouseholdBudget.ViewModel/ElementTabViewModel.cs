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
    public abstract class ElementTabViewModel<T> : ViewModelBase, ITabViewModel
    {


        public ElementTabViewModel(T currentItem)
        {

            CurrentItem = currentItem;

            EditItem = CopyItem(currentItem);

            SaveItemCommand = AsyncCommand.Create(token => SaveItemAsync(token, EditItem));
            DeleteItemCommand = AsyncCommand.Create(token => DeleteItemAsync(token, EditItem));
        }

        protected abstract T CopyItem(T item);
        protected abstract Task SaveItemAsync(CancellationToken token, T item);
        protected abstract Task DeleteItemAsync(CancellationToken token, T item);

        public string Header { get; set; }
        public T CurrentItem { get; set; }
        public T EditItem { get; set; }
        public IAsyncCommand SaveItemCommand { get; private set; }
        public IAsyncCommand DeleteItemCommand { get; private set; }

    }
}
