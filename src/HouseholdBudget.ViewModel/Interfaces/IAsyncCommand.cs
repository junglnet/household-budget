using System.Threading.Tasks;
using System.Windows.Input;

namespace HouseholdBudget.ViewModel.Interfaces
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
        string Name { get; set; }
    }

}
