using LogParserAndReader.Models.Template;
using LogParserAndReader.ViewModels;
using SimpleUIElements.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogParserAndReader.Views.FilterControls
{
    /// <summary>
    /// Interaction logic for EnumFilterControl.xaml
    /// </summary>
    public partial class EnumFilterControl : BaseFilterControl
    {
        private const string ENUM_REGEX_PATTERN = @"^{([A-Za-z]*\.?)*(?:})?";
        private static readonly Regex _entryPatternRegex = new Regex(ENUM_REGEX_PATTERN, RegexOptions.Compiled);
        private readonly EnumFilterControlViewModel _viewModel = new();
        public EnumFilterControl(PropertyPatternEntry propertyPattern) : base(propertyPattern)
        {
            InitializeComponent();
            BuildCheckList();
            DataContext = _viewModel;
        }

        private void BuildCheckList()
        {
            _viewModel.ItemsList = new List<CheckBoxComboBoxValue>();
            if (_propertyPattern.PropertyType == PropertyTypeStringConstants.EnumString)
            {
                var matches = _entryPatternRegex.Match(_propertyPattern.AcceptableValues);
                var enumType = Type.GetType(matches.Value.Trim('{').Trim('}'));
                if (enumType != null && enumType.IsEnum)
                {
                    var availableValues = Enum.GetNames(enumType);
                    foreach(var availableValue in availableValues)
                    {
                        _viewModel.ItemsList.Add(new() { Label = availableValue, CheckBoxValue = true });
                    }
                }
            }
        }

        private void CheckBoxComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
    }
}
