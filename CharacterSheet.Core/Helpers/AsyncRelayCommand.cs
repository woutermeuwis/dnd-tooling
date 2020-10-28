using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using CharacterSheet.Core.Interfaces;

namespace CharacterSheet.Core.Helpers
{
    public class AsyncRelayCommand : IAsyncCommand
    {
        private bool _isExecuting;

        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        private readonly Action<Exception> _errorHandler;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null, Action<Exception> errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }


        public bool CanExecute()
        {
            if (_isExecuting)
                return false;

            if (_canExecute == null)
                return true;

            return _canExecute();
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }
            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter) => CanExecute();

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().SafeFireAndForget(_errorHandler);
        }
    }

    public class AsyncRelayCommand<T> : IAsyncCommand<T>
    {
        private bool _isExecuting;

        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly Action<Exception> _errorHandler;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute = null, Action<Exception> errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }


        public bool CanExecute(T parameter)
        {
            if (_isExecuting)
                return false;

            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }
            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if(parameter is T typedParameter)
                CanExecute(typedParameter);
            
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter is T typedParameter)
                ExecuteAsync(typedParameter).SafeFireAndForget(_errorHandler);
        }
    }
}
