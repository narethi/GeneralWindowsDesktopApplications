using LogParserAndReader.Controllers;
using SimpleUIElements.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogParserAndReader.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
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

        private FilterController _filterController;
        public FilterController FilterController => _filterController;

        private MainWindowViewModel() { _logsController = new(); _filterController = new();  }

        public MainWindowViewModel(LogFileController logsController, FilterController filterController) 
        {
            _logsController = logsController;
            _filterController = filterController;
        }

        internal void OpenLogFile(string logfileName)
        {
            _logsController.ReadFile(logfileName);
        }
    }
}
