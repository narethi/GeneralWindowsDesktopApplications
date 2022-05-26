using LogParserAndReader.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LogParserAndReader.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private LogFileController _logsController;
        public LogFileController LogsController => _logsController;

        private MainWindowViewModel() { }

        public MainWindowViewModel(LogFileController logsController) 
        {
            _logsController = logsController;
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
