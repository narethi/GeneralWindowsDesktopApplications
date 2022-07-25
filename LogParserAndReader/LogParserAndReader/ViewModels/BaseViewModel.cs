using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogParserAndReader.ViewModels
{
    /// <summary>
    /// This is the base class that can be used anytime INotifyProperty is needed
    /// </summary>
    internal class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
