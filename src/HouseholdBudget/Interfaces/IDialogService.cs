using HouseholdBudget.Common.Entities;
using HouseholdBudget.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudget.Interfaces
{
    public interface IDialogService
    {

        void DeleteItemDialog(
           string itemName,
           Action<CustomDialogViewModel> OnOk = null,
           Action<CustomDialogViewModel> OnCancel = null,
           Action<CustomDialogViewModel> OnCloseRequest = null);

        void SaveItemDialog(
           string itemName,
           Action<CustomDialogViewModel> OnOk = null,
           Action<CustomDialogViewModel> OnCancel = null,
           Action<CustomDialogViewModel> OnCloseRequest = null);

       // void ItemRemoveErrorDialog(RemoveRelationItemExeption ex);
        void ErrorDialog(Exception ex);

       
        string OpenFileDialog();

    }
}
