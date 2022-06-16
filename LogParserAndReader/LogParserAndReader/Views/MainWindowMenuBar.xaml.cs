using Microsoft.Win32;
using SimpleUIElements.CustomRoutedEventArgs;
using System.Windows;
using System.Windows.Controls;

namespace LogParserAndReader.Views
{
    /// <summary>
    /// Interaction logic for MainWindowMenuBar.xaml
    /// </summary>
    public partial class MainWindowMenuBar : UserControl
    {
        public MainWindowMenuBar()
        {
            InitializeComponent();
        }

        #region RoutedEvents

        public static readonly RoutedEvent OpenFileEvent = 
            EventManager.RegisterRoutedEvent(nameof(OpenFile), RoutingStrategy.Bubble, typeof(RoutedEventHandler),typeof(MainWindowMenuBar));

        public event RoutedEventHandler OpenFile
        {
            add { AddHandler(OpenFileEvent, value); }
            remove { RemoveHandler(OpenFileEvent, value); }
        }

        #endregion
        
        private void OpenLogFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var logfileName = openFileDialog.FileName;
                var openFileEventArgs = new StringParamRoutedEventArgs(OpenFileEvent, logfileName);
                RaiseEvent(openFileEventArgs);
            }
        }
    }
}
