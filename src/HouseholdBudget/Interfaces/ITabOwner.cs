using HouseholdBudget.ViewModel;
using System.Collections.ObjectModel;

namespace HouseholdBudget.Interfaces
{
    public interface ITabOwner
    {

        ObservableCollection<FundTabViewModel> Tabs { get; }
        FundTabViewModel CurrentTabViewModel { get; set; }

    }
}
