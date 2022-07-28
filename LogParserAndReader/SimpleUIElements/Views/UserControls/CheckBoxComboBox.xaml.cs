using SimpleUIElements.Helpers;
using SimpleUIElements.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleUIElements.Views.UserControls
{
    /// <summary>
    /// Interaction logic for CheckBoxComboBox.xaml
    /// </summary>
    public partial class CheckBoxComboBox : ComboBox, INotifyPropertyChanged
    {
        public const string AllLabel = "ALL";
        
        /// <summary>
        /// This is the display value for the selected item entry in the combobox
        /// This is a semi-colon separated list of all labels
        /// </summary>
        public string DisplayedValue
        {
            get
            {
                var stringBuilder = new StringBuilder();
                if (ItemsSource is List<CheckBoxComboBoxValue> valuesList)
                { 
                    foreach (CheckBoxComboBoxValue item in ItemsSource)
                    {
                        if(valuesList.IndexOf(item) == 0 && item.CheckBoxValue)
                            return item.Label;
                        if(item.CheckBoxValue)
                            stringBuilder.Append($"{item.Label}; ");
                    }
                }
                if (stringBuilder.Length == 0)
                    return "None";
                return stringBuilder.ToString().Trim().TrimEnd(';');
            }
        }

        #region Dependency Properties

        /// <summary>
        /// The dependency property that is used to set what options for the dropdown
        /// </summary>
        public static readonly DependencyProperty CheckBoxValuesProperty =
            DependencyProperty.Register(nameof(CheckBoxValues),
            typeof(List<CheckBoxComboBoxValue>),
            typeof(CheckBoxComboBox), new FrameworkPropertyMetadata() { PropertyChangedCallback = OnDependencyPropertyChanged }
            );

        /// <summary>
        /// Holds the property that the CheckBoxValuesProperty that is registered to
        /// </summary>
        public List<CheckBoxComboBoxValue> CheckBoxValues
        {
            get => (List<CheckBoxComboBoxValue>)GetValue(CheckBoxValuesProperty);
            set
            {
                SetValue(CheckBoxValuesProperty, value);
                OnPropertyChanged();
            }
        }

        #endregion

        public CheckBoxComboBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets a check box while disabling the checked/unchecked events
        /// This is used primarily when changing the value of the "All" checkbox
        /// </summary>
        /// <param name="parentPanel">This is the item that contains the checkbox that needs to be unsubscribed from the events and updated </param>
        /// <param name="updateValue">This is the true/false value that the checkbox will be set to</param>
        private void SetCheckBoxValueWithDisabledChangeEvent(ComboBoxItem parentItem, bool updateValue)
        {
            var checkBox = (CheckBox)VisualTreeHelper.GetChild(parentItem, 0);
            if (updateValue)
            {
                checkBox.Checked -= CheckBox_Checked;
                checkBox.IsChecked = updateValue;
                checkBox.Checked += CheckBox_Checked;
            }
            else
            {
                checkBox.Unchecked -= CheckBox_Checked;
                checkBox.IsChecked = updateValue;
                checkBox.Unchecked += CheckBox_Checked;
            }
        }

        /// <summary>
        /// The check/uncheck routed event function that all the checkboxes are subcribed to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(e.Source is CheckBox box && 
                box.DataContext is CheckBoxComboBoxValue value && 
                ItemsSource is List<CheckBoxComboBoxValue> valuesList)
            {
                //This is the All Checkbox, if this check box is changed then select or deselect all check boxes
                if (valuesList.IndexOf(value) == 0)
                {
                    var parentPanel = ObjectHelper.FindFirstParentOfType<StackPanel>(box);
                    for (var index = 0; index < valuesList.Count; index++)
                    {
                        if (index != 0 && 
                            valuesList[index].CheckBoxValue != value.CheckBoxValue && 
                            parentPanel != null)
                            SetCheckBoxValueWithDisabledChangeEvent((ComboBoxItem)parentPanel.Children[index], value.CheckBoxValue);
                    }
                }
                else if ((valuesList[0].CheckBoxValue && !value.CheckBoxValue) ||
                         (!valuesList[0].CheckBoxValue &&
                            valuesList.Count(x => x.CheckBoxValue) == valuesList.Count - 1 &&
                            value.CheckBoxValue))
                {
                    //Find the "ALL" Checkbox, and turn it on or off
                    //If all other values are turned on, then turn on the "ALL" checkbox
                    //If "ALL" is on and any check box is turned off, then turn off the "ALL" checkbox
                    var parentPanel = ObjectHelper.FindFirstParentOfType<StackPanel>(box);
                    if(parentPanel != null) 
                        SetCheckBoxValueWithDisabledChangeEvent((ComboBoxItem)parentPanel.Children[0], value.CheckBoxValue);
                }
            }
            var selectionEvent = new SelectionChangedEventArgs(SelectionChangedEvent, new List<string>(), new List<string>());
            RaiseEvent(selectionEvent);
            OnPropertyChanged(nameof(DisplayedValue));
        }

        /// <summary>
        /// Read changes to the dependency properties and update the CheckBoxComboBoxControl
        /// </summary>
        /// <param name="sender">CheckBoxComboBox</param>
        /// <param name="e">The arguments for the new property information</param>
        private static void OnDependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is CheckBoxComboBox control)
            {
                if(e.Property.Name == nameof(CheckBoxValues))
                {
                    if(e.NewValue is List<CheckBoxComboBoxValue> values && values.Any())
                    {
                        if(values[0].Label != AllLabel)
                        {
                            values.Insert(0, new CheckBoxComboBoxValue() { Label = AllLabel, CheckBoxValue = values.All(x => x.CheckBoxValue) } );
                        }
                        control.ItemsSource = values;
                        control.SelectedIndex = 0;
                        control.OnPropertyChanged(nameof(ItemsSource));
                    }
                }
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
