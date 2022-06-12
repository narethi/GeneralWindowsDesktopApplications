using LogParserAndReader.ViewModels;
using SimpleUIElements.Views;
using System.Windows.Controls;

namespace LogParserAndReader.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomCaptionWindow
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = _viewModel;
            _captionBarControl = new MainWindowMenuBar();
            InitializeComponent();
        }
    }
}
