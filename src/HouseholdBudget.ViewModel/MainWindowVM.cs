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

            Transactions = new ObservableCollection<Transaction>()
            {
                new Transaction()
                {
                    Name = "Tesd"
                }
            };
              
            
            AppFactoty.Init();
                       
           // var tr = AppFactoty.Current.TransactionService.GetAllAsync().GetAwaiter().GetResult();


        }

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
        
        public ObservableCollection<Transaction> Transactions { get; set; }

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
