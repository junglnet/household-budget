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

namespace HouseholdBudget.ViewModel
{
    public class AppMainWindow : INotifyPropertyChanged
    {

        public AppMainWindow()
        {

            Transactions = new ObservableCollection<Transaction>()
            {
                new Transaction()
                {
                    Name = "Tesd"
                }
            };
                
            
            AppFactoty.Init();

            

            //Transactions = (ObservableCollection<Transaction>)Factory.Current.TransactionService.GetAllAsync().GetAwaiter().GetResult();


        }

        public async Task Init()
        {
            Transactions = (ObservableCollection<Transaction>)await Factory.Current.TransactionService.GetAllAsync();
        }

        public ObservableCollection<Transaction> Transactions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
