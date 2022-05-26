using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                OnPropertyChanged(nameof(ApplicationIconVerticalAlignment));
            }
        }

        /// <summary>
        /// This will set the vertical position of the size toolbar, if the window is maximized center the tool bar as there is a
        /// bug in windows that causes the tool bar to get pushed up
        /// </summary>
        public VerticalAlignment ApplicationIconVerticalAlignment => _isWindowMaximized ? VerticalAlignment.Center : VerticalAlignment.Center;

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
