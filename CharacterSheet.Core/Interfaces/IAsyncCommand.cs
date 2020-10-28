using System.Threading.Tasks;
using System.Windows.Input;

namespace CharacterSheet.Core.Interfaces
{
    interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }
}
