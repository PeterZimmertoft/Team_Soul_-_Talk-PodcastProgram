using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;



        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter) 
        {
            execute(parameter);
        }


        public event EventHandler CanExecuteChanged;
        
        
    }
}
