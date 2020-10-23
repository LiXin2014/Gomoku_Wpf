using System;
using System.Windows.Input;

namespace Gomoku
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// CanExecute changed event, this is bind to button's IsEnabled property
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private Action<object> execute;
        private Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if(execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Raise can excute changed event
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Gives if the command can be executed
        /// </summary>
        /// <param name="parameter">parameter sent from a specific command</param>
        /// <returns>true if it can be executed, false otherwise</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">parameter sent from a specific command</param>
        public void Execute(object parameter)
        {
            execute(parameter);
            RaiseCanExecuteChanged();
        }
    }
}
