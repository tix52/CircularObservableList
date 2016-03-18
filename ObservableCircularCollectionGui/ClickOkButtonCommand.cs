using System;
using System.Windows.Input;

namespace ObservableCircularCollectionGui
{
    class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<object> _action;
        Predicate<object> _canExecute;

        public CommandHandler(Action<object> action, Predicate<object> canExecute)
        {
            _canExecute = canExecute;
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
