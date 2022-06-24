using LogParserAndReader.Controllers;
using SimpleUIElements.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogParserAndReader.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<CheckBoxComboBoxValue> _testItemsList = new()
        {
            new CheckBoxComboBoxValue() { Label = "Test1", CheckBoxValue = true },
            new CheckBoxComboBoxValue() { Label = "test2", CheckBoxValue = true },
            new CheckBoxComboBoxValue() { Label = "Test1", CheckBoxValue = true },
            new CheckBoxComboBoxValue() { Label = "test2", CheckBoxValue = true }
        };

        public List<CheckBoxComboBoxValue> TestItemsList
        {
            get => _testItemsList; 
            set 
            { 
                _testItemsList = value;
                OnPropertyChanged();
            }
        }

        private LogFileController _logsController;
        public LogFileController LogsController => _logsController;

        private MainWindowViewModel() { _logsController = new(); }

        public MainWindowViewModel(LogFileController logsController) 
        {
            _logsController = logsController;
        }

        internal void OpenLogFile(string logfileName)
        {
            _logsController.ReadFile(logfileName);
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
