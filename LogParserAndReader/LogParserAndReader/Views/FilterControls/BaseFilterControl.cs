using SimpleUIElements.Delegates;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace LogParserAndReader.Views.FilterControls
{
    public abstract class BaseFilterControl : UserControl, INotifyPropertyChanged
    {
        public VoidDelegate ClickFunction => HideClick;

        private string _controlName = "Not Set";
        public string ControlName
        {
            get => _controlName;
            set
            {
                _controlName = value;
                OnPropertyChanged();
            }
        }

        protected BaseFilterControl()
        {
            var myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source =
                new Uri("/LogParserAndReader;component/Views/FilterControls/FilterControlResource.xaml",
                        UriKind.RelativeOrAbsolute);
            var windowStyle = myResourceDictionary[nameof(BaseFilterControl)];
            if (windowStyle != null)
                Style = (Style)windowStyle;
        }

        private void HideClick()
        {
            Debug.WriteLine("Button pushed");
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
