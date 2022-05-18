using LogParserAndReader.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LogParserAndReader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var test = ApplicationConfigurations.Instance.ReadLogFileTemplateFromFile("Test.template");
            var logFileController = new LogFileController();
            logFileController.ReadFile("Test.Log");
            var mainWindow = new MainWindow(new ViewModels.MainWindowViewModel(logFileController));
            mainWindow.Show();
        }
    }
}
