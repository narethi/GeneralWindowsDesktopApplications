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
        private const int MaxControlWidth = 175;
        private const int MinControlWidth = 38;
        public VoidDelegate ClickFunction => HideClick;

        private string _controlName = "Not Set";
        
        /// <summary>
        /// This is the label for the filter control
        /// </summary>
        public string ControlName
        {
            get => _controlName;
            set
            {
                _controlName = value;
                OnPropertyChanged();
            }
        }
        private bool _controlHidden = false;
        private int _controlWidth = MaxControlWidth;
        public int ControlWidth
        {
            get => _controlWidth;
            set
            {
                _controlWidth = value;
                OnPropertyChanged();
            }
        }

        private string _buttonString = "v";
        public string ButtonString
        {
            get => _buttonString;
            set
            {
                _buttonString = value;
                OnPropertyChanged();
            }
        }

        public Visibility ControlHidden => _controlHidden ? Visibility.Collapsed : Visibility.Visible;

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

        /// <summary>
        /// This is the click function used to hide and show the filter control
        /// </summary>
        private void HideClick()
        {
            _controlHidden = !_controlHidden;
            if(_controlHidden)
            {
                ControlWidth = MinControlWidth;
                ButtonString = "^";
            }
            else
            {
                ControlWidth = MaxControlWidth;
                ButtonString = "v";
            }
            OnPropertyChanged(nameof(ControlHidden));
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
