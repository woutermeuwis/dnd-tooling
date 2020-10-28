using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CharacterSheet.Core.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName, object before, object after)
        {
            OnPropertyChanged(propertyName);
        }

        protected virtual void Log(string text) => System.Diagnostics.Debug.WriteLine(text);
    }
}
