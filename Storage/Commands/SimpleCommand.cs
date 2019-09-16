using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Storage.Commands
{
    public class SimpleCommand : ICommand
    {
        public Boolean CommandSucceeded { get; set; }

        public Func<object, bool> CanExecuteDelegate { get; set; }

        public Action<object> ExecuteDelegate { get; set; }

        public bool IsExecute { get; set; }

        public SimpleCommand(Action p_action, Func<object, bool> canExecute = null)
        {
            ExecuteDelegate = o => p_action();
            CanExecuteDelegate = canExecute;
            IsExecute = true;
        }
        public SimpleCommand(Action<object> p_action, Func<object, bool> canExecute = null)
        {
            ExecuteDelegate = p_action;
            CanExecuteDelegate = canExecute;
            IsExecute = true;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate == null || CanExecuteDelegate(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            if (!IsExecute) return;
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
                CommandSucceeded = true;
            }
        }
        
    }
}
