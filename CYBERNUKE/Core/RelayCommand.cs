using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CYBERNUKE.Core
{
    // From Documentation:
    /// <summary>
    /// The RelayCommand and RelayCommand<T> are ICommand implementations that can expose a method or delegate to the view. 
    /// These types act as a way to bind commands between the viewmodel and UI elements.
    /// https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/relaycommand
    /// </summary>
    class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
