using System.Collections.ObjectModel;

namespace HouseholdBudget.ViewModel.Interfaces
{
    public interface ITabOwner
    {

        ObservableCollection<FundTabListViewModel> Tabs { get; set; }
        FundTabListViewModel CurrentTabViewModel { get; set; }

    }
}
