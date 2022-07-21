using LogParserAndReader.Controllers;
using LogParserAndReader.Exceptions;
using LogParserAndReader.Views;
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

            if(ApplicationConfigurations.Instance.ReadLogFileTemplateFromFile("Test.template"))
            {
                var logFileController = new LogFileController();
                logFileController.ReadFile("Test.Log");
                var filterController = new FilterController();
                var mainWindow = new MainWindow(new ViewModels.MainWindowViewModel(logFileController, filterController));
                mainWindow.Show();
            }
            else
                throw new FailedApplicationStartUpException();
        }
    }
}
