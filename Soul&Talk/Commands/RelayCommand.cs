using System;
using System.Windows.Input;


namespace Soul_Talk.Commands
{
    // RelayCommand er en implementering af ICommand, der gør det nemt at binde handlinger i ViewModel til UI-elementer i XAML.
    // Den tager en Action (eller Action<object?>) for at definere, hvad der skal ske, når kommandoen udføres, og en valgfri Predicate<object?> for at bestemme, om kommandoen kan udføres
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public RelayCommand(Action execute)
            : this(parameter => execute(), null) // Denne constructor tager en parameterløs Action og omdanner den til en Action<object?>, der ignorerer parameteren.
                                                 // Det gør det nemt at bruge RelayCommand med metoder, der ikke kræver input. 
        {}

        public RelayCommand(Action<object?> execute) : this(execute, null) {}

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
