using System;
using System.Windows.Input;


namespace HouseholdBudget.Entities
{
    public class RelayCommand : ICommand
    {

        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, string name, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            Name = name;
        }



        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public string Name { get; set; }
    }
}
