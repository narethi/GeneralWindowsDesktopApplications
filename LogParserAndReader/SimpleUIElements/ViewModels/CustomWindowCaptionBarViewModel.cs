using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleUIElements.ViewModels
{
    internal class CustomWindowCaptionBarViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The icon for the application, if the icon is not set then use the empty icon
        /// </summary>
        public ImageSource ApplicationIcon
        {
            get
            {
                var currentAssembly = System.Reflection.Assembly.GetEntryAssembly();
                System.IntPtr iconHandle = (System.IntPtr)0;
                if(currentAssembly != null)
                {
                    var icon = Icon.ExtractAssociatedIcon(currentAssembly.Location.Replace(".dll", ".exe"));
                    if(icon != null)
                        iconHandle = icon.Handle;
                }

                if(iconHandle == (System.IntPtr)0)
                {
                    iconHandle = Properties.Resources.TransparentTile.GetHicon();
                }
                return Imaging.CreateBitmapSourceFromHIcon(iconHandle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        /// <summary>
        /// The windows custom window colour
        /// </summary>
        public System.Windows.Media.Brush SystemWindowColor => SystemParameters.WindowGlassBrush;

        private bool _isWindowMaximized = false;

        /// <summary>
        /// "One Way to Source" property used to set the restore/maximize button visibility
        /// </summary>
        public bool IsWindowMaximized
        {
            set
            {
                _isWindowMaximized = value;
                OnPropertyChanged(nameof(RestoreWindowButtonVisibility));
                OnPropertyChanged(nameof(MaximizeWindowButtonVisibility));
            }
        }

        private UIElement? _captionCustomControl;

        /// <summary>
        /// This is the property that stores the custom control in the caption bar
        /// </summary>
        public UIElement CaptionCustomControl
        {
            get => _captionCustomControl ?? new();
            set
            {
                _captionCustomControl = value;
                OnPropertyChanged();
            }
        }

        private string _windowName = string.Empty;

        /// <summary>
        /// This is the title of the window, this will append the word "[Administrator]" when the application is in admin mode
        /// </summary>
        public string WindowName
        {
            get => _windowName;
            set
            {
                if(new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                {
                    value = $"{value} [Administrator]";
                }
                _windowName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// One way property that determines the visibility of the Restore Window Visibility (If the window is maximized show the button)
        /// </summary>
        public Visibility RestoreWindowButtonVisibility => _isWindowMaximized ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// One way property that determines the visibility of the Maximize Window Visibility (If the window is not maximized show the button)
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
