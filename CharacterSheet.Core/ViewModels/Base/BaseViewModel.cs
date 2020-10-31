using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CharacterSheet.Core.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region LifeCycle

        public virtual void Prepare()
        {

        }

        public virtual Task Initialize()
        {
            return Task.CompletedTask;
        }

        #endregion

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName, object before, object after)
        {
            OnPropertyChanged(propertyName);
        }
        #endregion

        #region Logging

        protected virtual void Log(string text) => System.Diagnostics.Debug.WriteLine(text);

        #endregion
    }

    public class BaseViewModel<T> : BaseViewModel
    {
        public virtual void Prepare(T param)
        {

        }
    }
}
