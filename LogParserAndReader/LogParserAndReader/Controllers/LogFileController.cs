using LogParserAndReader.Models.LogFile;
using LogParserAndReader.Parsers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogParserAndReader.Controllers
{
    public class LogFileController : INotifyPropertyChanged
    {
        public delegate void VoidHandler();
        public event VoidHandler? LogFileLoaded;

        private LogFile _loadedFile = new();
        public LogFile LoadedFile
        {
            get => _loadedFile;
            set 
            {
                _loadedFile = value;
                if(LogFileLoaded != null)
                {
                    LogFileLoaded();
                }
                OnPropertyChanged();
            }
        }
        public void ReadFile(string fileName)
        {
            LoadedFile = LogFileParser.ReadLogsFromFile(fileName);
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
