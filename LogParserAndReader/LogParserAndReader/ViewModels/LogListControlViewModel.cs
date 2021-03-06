using LogParserAndReader.Controllers;

namespace LogParserAndReader.ViewModels
{
    /// <summary>
    /// ViewModel that manages the data for the log list control
    /// </summary>
    internal class LogListControlViewModel : BaseViewModel
    {
        private LogFileController? _logsController;

        /// <summary>
        /// This is the log file controller, this should be the controller for the application
        /// </summary>
        public LogFileController? LogsController
        { 
            get => _logsController; 
            set
            {
                _logsController = value;
                OnPropertyChanged();
            }
        }
    }
}
