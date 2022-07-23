using LogParserAndReader.ViewModels;
using SimpleUIElements.CustomRoutedEventArgs;
using SimpleUIElements.Views;
using System;
using System.Windows;

namespace LogParserAndReader.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomCaptionWindow
    {
        private readonly MainWindowViewModel _viewModel;
        internal MainWindow(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = _viewModel;
            var menu = new MainWindowMenuBar();
            menu.OpenFile += Menu_OpenFile;
            _captionBarControl = menu;
            InitializeComponent();
        }

        private void Menu_OpenFile(object sender, RoutedEventArgs e)
        {
            if(e is StringParamRoutedEventArgs stringParam)
            {
                _viewModel.OpenLogFile(stringParam.Value);
            }
        }

        private void CheckBoxComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
    }
}
