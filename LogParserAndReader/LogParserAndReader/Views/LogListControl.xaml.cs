using LogParserAndReader.Controllers;
using LogParserAndReader.ViewModels;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace LogParserAndReader.Views
{
    /// <summary>
    /// Interaction logic for LogListControl.xaml
    /// </summary>
    public partial class LogListControl : UserControl
    {
        private readonly LogListControlViewModel _viewModel = new();
        public LogListControl()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }

        #region Dependency Properties

        public static readonly DependencyProperty LogsControllerProperty =
            DependencyProperty.Register(nameof(LogsController),
            typeof(LogFileController),
            typeof(LogListControl), new FrameworkPropertyMetadata() { PropertyChangedCallback = OnDependencyPropertyChanged }
        );

        /// <summary>
        /// This is the backing for the LogsControllerProperty
        /// </summary>
        public LogFileController? LogsController
        {
            get => _viewModel.LogsController;
            set => SetValue(LogsControllerProperty, value);    
        }

        #endregion

        /// <summary>
        /// Reads the data from the loaded log file and then constructs the list control to display the log file data
        /// </summary>
        /// <remarks>
        /// This can move to the view model, the DataTable object isn't a UIElement and can be bound to the DataGrid
        /// </remarks>
        private void ConstructListControl()
        {
            /*
             * 1: Read the template and figure out which column is required
             * 2: Construct the new list control
             * 3: add the required columns
             * 4: add in the data for the rows
             */

            DataTable table = new(); 
            var template = ApplicationConfigurations.Instance.LogFileTemplate;

            foreach (var listColumn in template)
            {

                table.Columns.Add(new DataColumn()
                {
                    ColumnName = listColumn.PropertyName,
                    ReadOnly = true,
                });
            }

            if(LogsController != null)
            {
                foreach (var entry in LogsController.LoadedFile.LogFileEntries)
                {
                    var data = table.NewRow();
                    foreach (var column in entry.LogFileProperties)
                    {
                        data[table.Columns.IndexOf(column.PropertyName)] = column.PropertyValue;
                    }
                    table.Rows.Add(data);
                }
            }
            
            LogEntryList.ItemsSource = table.DefaultView;
        }

        private static void OnDependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is LogListControl control)
            {
                if (e.Property.Name.Equals(nameof(LogsController)))
                {
                    if(control._viewModel.LogsController != null)
                        control._viewModel.LogsController.LogFileLoaded -= control.ConstructListControl;
                    control._viewModel.LogsController = (LogFileController)e.NewValue;
                    control.ConstructListControl();
                    control._viewModel.LogsController.LogFileLoaded += control.ConstructListControl;
                }
            }
        }
    }
}
