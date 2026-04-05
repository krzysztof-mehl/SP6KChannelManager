using System.Windows.Input;

namespace SP6KChannelManager.Commands
{
    public class RelayCommand(Action execute, Func<bool>? canExecute) : ICommand
    {
        private readonly Action _execute = execute;
        private readonly Func<bool>? _canExecute = canExecute;

        public RelayCommand(Action execute)
            : this(execute, (Func<bool>?)null)
        {
        }

        public RelayCommand(Action execute, bool canExecute)
            : this(execute, () => canExecute)
        {
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute();

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
