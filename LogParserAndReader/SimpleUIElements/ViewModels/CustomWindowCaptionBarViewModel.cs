using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleUIElements.ViewModels
{
    internal class CustomWindowCaptionBarViewModel : INotifyPropertyChanged
    {
        public ImageSource ApplicationIcon => Imaging.CreateBitmapSourceFromHIcon(
                    Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location.Replace(".dll", ".exe")).Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

        public System.Windows.Media.Brush SystemWindowColor => SystemParameters.WindowGlassBrush;

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

        private UIElement _captionCustomControl;

        public UIElement CaptionCustomControl
        {
            get => _captionCustomControl;
            set
            {
                _captionCustomControl = value;
                OnPropertyChanged();
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
