using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.ViewModels
{
    internal class StringFilterControlViewModel : BaseViewModel
    {
        private ObservableCollection<string> _strings = new();
        public ObservableCollection<string> Strings
        {
            get => _strings;
            set
            {
                _strings = value;
                OnPropertyChanged();
            }
        }

        private List<string> _selectedItems = new();
        public List<string> SelectedItems
        {
            get => _selectedItems;
            set
            { 
                _selectedItems = value;
                OnPropertyChanged();
            }
        }

        private string _filterValue = string.Empty;
        public string FilterValue
        {
            get => _filterValue;
            set
            {
                _filterValue = value;
                OnPropertyChanged();
            }
        }

        public void AddNewValueString()
        {
            if(!_strings.Contains(_filterValue))
            {
                Strings.Add(_filterValue);
                FilterValue = string.Empty;
            }
        }
    }
}