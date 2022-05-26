﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace SimpleUIElements.ViewModels
{
    internal class WindowSizeToolBarViewModel : INotifyPropertyChanged
    {
        private bool _isWindowMaximized = false;
        public bool IsWindowMaximized
        {
            set
            {
                _isWindowMaximized = value;
                OnPropertyChanged(nameof(RestoreWindowButtonVisibility));
                OnPropertyChanged(nameof(MaximizeWindowButtonVisibility));
            }
        }

        /// <summary>
        /// This will set the visibility of the Restore Window Visibility (If the window is maximized show the button)
        /// </summary>
        public Visibility RestoreWindowButtonVisibility => _isWindowMaximized ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// This will set the visibility of the Maximize Window Visibility (If the window is not maximized show the button)
        /// </summary>
        public Visibility MaximizeWindowButtonVisibility => _isWindowMaximized ? Visibility.Collapsed : Visibility.Visible;


        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
