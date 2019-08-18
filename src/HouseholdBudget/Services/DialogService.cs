using HouseholdBudget.Interfaces;
using HouseholdBudget.ViewModel;
using MvvmDialogs.ViewModels;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using HouseholdBudget.Common.Entities;

namespace HouseholdBudget.Services
{
        
    public class DialogService : IDialogService
    {
        private ObservableCollection<IDialogViewModel> _dialogs;

        public DialogService(ObservableCollection<IDialogViewModel> dialogs)
        {

            _dialogs = dialogs;

        }
        public void DeleteItemDialog (
            string itemName, 
            Action<CustomDialogViewModel> OnOk = null,
            Action<CustomDialogViewModel> OnCancel = null,
            Action<CustomDialogViewModel> OnCloseRequest = null)
        {
           

            _dialogs.Add(new CustomDialogViewModel
            {
                Message = "Действительно удалить " + itemName + "?",

                Caption = "Удаление объекта справочника",

                OnOk = (sender) =>
                {

                    OnOk?.Invoke(sender);

                    sender.Close();

                },

                OnCancel = (sender) =>
                {

                    OnCancel?.Invoke(sender);
                    
                    sender.Close();

                },

                OnCloseRequest = (sender) =>
                {

                    OnCloseRequest?.Invoke(sender);                    

                    sender.Close();

                }

            });

           
        }

        public void SaveItemDialog (
            string itemName,
            Action<CustomDialogViewModel> OnOk = null,
            Action<CustomDialogViewModel> OnCancel = null,
            Action<CustomDialogViewModel> OnCloseRequest = null)
        {
            
            _dialogs.Add(new CustomDialogViewModel

            {
                Message = "Объект " + itemName + " был изменен.\n Сохранить изменения?",

                Caption = "Сохранение изменений",

                OnOk = (sender) =>
                {
                    OnOk?.Invoke(sender);

                    sender.Close();

                },

                OnCancel = (sender) =>
                {

                    OnCancel?.Invoke(sender);

                    sender.Close();

                },

                OnCloseRequest = (sender) =>
                {

                    OnCloseRequest?.Invoke(sender);

                    sender.Close();

                }

            });
           
        }

        //public void ItemRemoveErrorDialog(RemoveRelationItemExeption ex)
        //{
        //    string _message = ex.Message;

        //    string[] _errorString = ex.RelationList.Select(obj => obj.ToString()).ToArray();

        //    _message += Environment.NewLine + string.Join(Environment.NewLine, _errorString);

        //    _dialogs.Add(new MinimalDialogViewModel(_message, "Ошибка удаления")

        //    {

        //        Image = System.Windows.MessageBoxImage.Error,
        //        OnOk = (sender) =>
        //        {
                    
        //            sender.Close();

        //        },

        //    });

        //}

        public void ErrorDialog(Exception ex)
        {
            string _message = ex.Message;           

            _dialogs.Add(new MessageBoxViewModel(_message, "Ошибка")

            {

                Image = System.Windows.MessageBoxImage.Error                

            });

        }

        
        public string OpenFileDialog()
        {
            // TODO: Реализовать передачу Filter по типу загружаемого файла.
            var openFileDialog = new OpenFileDialogViewModel()
            {

                Title = "Открытие файал",
                Filter = "All files (*.*)|*.*",
                Multiselect = false

            };


            if (openFileDialog.Show(_dialogs))
                return openFileDialog.FileName;

            else
                return null;
                                          
        }

        public string SaveFileDialog(string startFileName)
        {

            var saveFileDialog = new SaveFileDialogViewModel()
            {

                Title = "Сохранение файла",
                FileName = startFileName,
                Filter = "All files (*.*)|*.*",
                Multiselect = false

            };


            if (saveFileDialog.Show(_dialogs))
                return saveFileDialog.FileName;

            else
                return null;

        }

    }
}
