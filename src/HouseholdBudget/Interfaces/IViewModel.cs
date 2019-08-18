using MvvmDialogs.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace HouseholdBudget.Interfaces
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ObservableCollection<IDialogViewModel> Dialogs { get; }
    }
}
