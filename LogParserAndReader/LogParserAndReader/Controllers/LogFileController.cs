using LogParserAndReader.Models.LogFile;
using LogParserAndReader.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.Controllers
{
    public class LogFileController : INotifyPropertyChanged
    {
        private LogFile _loadedFile;
        public LogFile LoadedFile
        {
            get => _loadedFile;
            set 
            {
                _loadedFile = value;
                OnPropertyChanged();
            }
        }
        public void ReadFile(string fileName)
        {
            _loadedFile = LogFileParser.ReadLogsFromFile(fileName);
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
