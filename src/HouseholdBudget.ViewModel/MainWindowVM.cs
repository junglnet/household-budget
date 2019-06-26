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
using System.Windows;

namespace HouseholdBudget.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {

        public MainWindowVM()
        {

            AppFactoty.Init();

            NotifyTaskCompletion 
                = new NotifyTaskCompletion<IReadOnlyList<Transaction>>(
                    AppFactoty.Current.TransactionService.GetAllAsync());
        //    Transactions = new ObservableCollection<Transaction>(NotifyTaskCompletion.Result);

         //   Init();

        }

        public async void Init()
        {
            
            var tmp = await AppFactoty.Current.TransactionService.GetAllAsync();
            Transactions = new ObservableCollection<Transaction>(tmp);
          //  OnPropertyChanged("Transactions");
        }

        public NotifyTaskCompletion<IReadOnlyList<Transaction>> NotifyTaskCompletion { get; private set; }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Transaction tr = new Transaction()
                        {
                            Name = "sdd"
                        };
                        Transactions.Add(tr);
                        SelectedPhone = tr;
                    }));
            }
        }
        
        public ObservableCollection<Transaction> Transactions
        {
            get
            {
                return transactions;
            }
            set
            {
                transactions = value;
                OnPropertyChanged("Transactions");
            }
        }

        private ObservableCollection<Transaction> transactions;

        public Transaction SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        private Transaction selectedPhone;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
