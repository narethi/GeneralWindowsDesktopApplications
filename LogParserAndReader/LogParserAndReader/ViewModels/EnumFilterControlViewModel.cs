using SimpleUIElements.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.ViewModels
{
    internal class EnumFilterControlViewModel : BaseViewModel
    {
        private List<CheckBoxComboBoxValue> _itemsList = new()
        {
            new CheckBoxComboBoxValue() { Label = "Test1", CheckBoxValue = true },
            new CheckBoxComboBoxValue() { Label = "test2", CheckBoxValue = true },
            new CheckBoxComboBoxValue() { Label = "Test1", CheckBoxValue = true },
            new CheckBoxComboBoxValue() { Label = "test2", CheckBoxValue = true }
        };

        public List<CheckBoxComboBoxValue> ItemsList
        {
            get => _itemsList;
            set
            {
                _itemsList = value;
                OnPropertyChanged();
            }
        }
    }
}
