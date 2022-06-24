using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleUIElements.Model
{
    /// <summary>
    /// This stores the data for a combo box item in the CheckBoxComboBox
    /// </summary>
    public class CheckBoxComboBoxValue : INotifyPropertyChanged
    {
        private bool _checkBoxValue = false;

        /// <summary>
        /// This is the actual value that is stored in the check box
        /// </summary>
        public bool CheckBoxValue 
        {
            get => _checkBoxValue;
            set
            {
                _checkBoxValue = value;
                OnPropertyChanged();
            }
        }

        private string _label = string.Empty;
        
        /// <summary>
        /// This is the label for the check box
        /// </summary>
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
