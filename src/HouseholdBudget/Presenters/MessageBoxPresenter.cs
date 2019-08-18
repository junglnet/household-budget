using HouseholdBudget.ViewModel;
using MvvmDialogs.Presenters;
using System.Windows;

namespace HouseholdBudget.Presenters
{
    public class MessageBoxPresenter : IDialogBoxPresenter<MessageBoxViewModel>
    {
        public void Show(MessageBoxViewModel vm)
        {
            vm.Result = MessageBox.Show(vm.Message, vm.Caption, vm.Buttons, vm.Image);
        }
    }
}
