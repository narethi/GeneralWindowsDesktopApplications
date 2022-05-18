using LogParserAndReader.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.ViewModels
{
    public class MainWindowViewModel
    {
        private LogFileController _logsController;
        public LogFileController LogsController => _logsController;

        private MainWindowViewModel() { }

        public MainWindowViewModel(LogFileController logsController) 
        {
            _logsController = logsController;
        }
    }
}
