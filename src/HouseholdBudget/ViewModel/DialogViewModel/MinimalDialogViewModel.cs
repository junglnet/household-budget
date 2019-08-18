using HouseholdBudget.Entities;
using MvvmDialogs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HouseholdBudget.ViewModel
{
	public class MinimalDialogViewModel : ViewModelBase, IUserDialogViewModel
    {

        public MinimalDialogViewModel(string message = "", string caption = "")
        {
            Message = message;
            Caption = caption;
        }

        public ICommand OkCommand { get { return new RelayCommand(obj => Ok()); } }
        protected virtual void Ok()
        {
            Close();
            if (this.OnOk != null)
                this.OnOk(this);
        }

        public virtual bool IsModal { get { return true; } }

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


        public void Close()
        {
            if (this.DialogClosing != null)
                this.DialogClosing(this, new EventArgs());
        }

        public void Show(IList<IDialogViewModel> collection)
        {
            collection.Add(this);
        }

        public Action<MinimalDialogViewModel> OnOk { get; set; }

        public virtual void RequestClose() { this.DialogClosing(this, null); }

		public virtual event EventHandler DialogClosing;

        private MessageBoxImage _Image = MessageBoxImage.None;
        public MessageBoxImage Image
        {
            get { return _Image; }
            set { _Image = value; }
        }


    }
}
