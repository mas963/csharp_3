using System.Windows.Input;

namespace WpfApp1.RelayCommands;

class RelayCommand : ICommand
{
    Action<object> _execute;
    Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
        }
        remove
        {
            CommandManager.RequerySuggested -= value;
        }
    }

    public bool CanExecute(object? parameter)
    {
        if (_canExecute != null)
        {
            return _canExecute(parameter);
        }
        else
        {
            return false;
        }
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }
}
