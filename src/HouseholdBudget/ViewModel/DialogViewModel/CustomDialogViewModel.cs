using HouseholdBudget.Entities;
using MvvmDialogs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HouseholdBudget.ViewModel
{
    public class CustomDialogViewModel : ViewModelBase, IUserDialogViewModel
    {
        #region IUserDialogViewModel Implementation

        public bool IsModal { get; private set; }
        public virtual void RequestClose()
        {
            if (this.OnCloseRequest != null)
                this.OnCloseRequest(this);
            else
                Close();
        }
        public event EventHandler DialogClosing;


        #endregion IUserDialogViewModel Implementation

        #region Commands

        public ICommand OkCommand { get { return new RelayCommand(obj => Ok()); } }
        protected virtual void Ok()
        {
            Close();
            if (this.OnOk != null)
                this.OnOk(this);
        }

        public ICommand CancelCommand { get { return new RelayCommand(obj => Cancel()); } }
        protected virtual void Cancel()
        {
            if (this.OnCancel != null)
                this.OnCancel(this);
            else
                Close();
        }

        public ICommand CloseCommand { get { return new RelayCommand(obj => Close()); } }

        #endregion Commands


        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; OnPropertyChanged(); }
        }

        private string _Caption;
        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; OnPropertyChanged(); }
        }

        public Action<CustomDialogViewModel> OnOk { get; set; }
        public Action<CustomDialogViewModel> OnCancel { get; set; }
        public Action<CustomDialogViewModel> OnCloseRequest { get; set; }

        public CustomDialogViewModel(bool isModal = true)
        {
            this.IsModal = isModal;
        }

        public void Close()
        {
            if (this.DialogClosing != null)
                this.DialogClosing(this, new EventArgs());
        }

        public void Show(IList<IDialogViewModel> collection)
        {
            collection.Add(this);
        }

    }
}
