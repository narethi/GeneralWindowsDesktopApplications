using LogParserAndReader.ViewModels;
using System.Collections.Generic;

namespace LogParserAndReader.Views.FilterControls
{
    /// <summary>
    /// Interaction logic for StringFilterControl.xaml
    /// </summary>
    public partial class StringFilterControl : BaseFilterControl
    {
        private StringFilterControlViewModel _viewModel = new();
        public StringFilterControl()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void AddNewFilterItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.AddNewValueString();
        }

        private void RemoveSelectedItems_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach(var item in _viewModel.SelectedItems)
            {
                _viewModel.Strings.Remove(item);
            }
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(e.AddedItems != null)
            {
                foreach(string item in e.AddedItems)
                {
                    _viewModel.SelectedItems.Add(item);
                }
            }
                
            if (e.RemovedItems != null && e.AddedItems is List<string> removedItems)
            {
                _viewModel.SelectedItems.RemoveAll(x => removedItems.Contains(x));            
            }
        }
    }
}
