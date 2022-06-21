using LogParserAndReader.Controllers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogParserAndReader.ViewModels
{
    /// <summary>
    /// ViewModel that manages the data for the log list control
    /// </summary>
    internal class LogListControlViewModel : INotifyPropertyChanged
    {
        private LogFileController? _logsController;

        /// <summary>
        /// This is the log file controller, this should be the controller for the application
        /// </summary>
        public LogFileController LogsController
        { 
            get => _logsController; 
            set
            {
                _logsController = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
