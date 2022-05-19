using LogParserAndReader.Controllers;
using LogParserAndReader.Models.LogFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.ViewModels
{
    internal class LogListControlViewModel : INotifyPropertyChanged
    {
        private LogFileController _logsController;

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
