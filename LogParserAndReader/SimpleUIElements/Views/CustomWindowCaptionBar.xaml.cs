using SimpleUIElements.Helpers;
using SimpleUIElements.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleUIElements.Views
{
    /// <summary>
    /// Interaction logic for WindowCustomCaptionBar.xaml
    /// </summary>
    public partial class CustomWindowCaptionBar : UserControl
    {
        private double _lastValidWidth = 0;
        private readonly CustomWindowCaptionBarViewModel _viewModel = new();
        private Window? _parentWindow;

        /// <summary>
        /// One way property that returns the parent window for this caption bar
        /// </summary>
        public Window ParentWindow
        {
            get
            {
                if(_parentWindow == null)
                {
                    _parentWindow = Window.GetWindow(this);
                }
                return _parentWindow;
            }
        }

        public CustomWindowCaptionBar()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        #region Dependency Properties

        public static readonly DependencyProperty CaptionCustomControlProperty =
            DependencyProperty.Register(nameof(CaptionCustomControl),
            typeof(UIElement),
            typeof(CustomWindowCaptionBar), new FrameworkPropertyMetadata() { PropertyChangedCallback = OnDependencyPropertyChanged }
        );

        private static void OnDependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is CustomWindowCaptionBar control)
            {
                if (e.Property.Name.Equals(nameof(CaptionCustomControl)))
                {
                    control._viewModel.CaptionCustomControl = (UIElement)e.NewValue;
                }
            }
        }

        public UIElement CaptionCustomControl
        {
            get => _viewModel.CaptionCustomControl;
            set => SetValue(CaptionCustomControlProperty, value);
        }

        #endregion

        private void CustomWindowCaptionBar_StateChanged(object? sender, EventArgs e)
        {
            if (ParentWindow.WindowState == WindowState.Maximized)
            {
                _viewModel.IsWindowMaximized = true;
            }
            else
            {
                _viewModel.IsWindowMaximized = false;
            }
        }

        private void CustomWindowCaptionBar_Loaded(object sender, RoutedEventArgs e)
        {
            ParentWindow.StateChanged += CustomWindowCaptionBar_StateChanged;
            ParentWindow.SizeChanged += CustomWindowCaptionBar_SizeChanged;
            _lastValidWidth = ParentWindow.ActualWidth;
            _viewModel.WindowName = ParentWindow.Title;
        }

        private void CustomWindowCaptionBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(ParentWindow.WindowState != WindowState.Maximized)
            {
                _lastValidWidth = ParentWindow.ActualWidth;
            }
        }

        private void CustomWindowCaptionBar_UnLoaded(object sender, RoutedEventArgs e)
        {
            ParentWindow.StateChanged -= CustomWindowCaptionBar_StateChanged;
            ParentWindow.SizeChanged -= CustomWindowCaptionBar_SizeChanged;
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if(e.RightButton != MouseButtonState.Pressed)
            {
                if (ParentWindow.WindowState == WindowState.Maximized)
                {
                    POINT test = new();
                    NativeMethods.GetCursorPos(ref test);
                    ParentWindow.Top = test.y - 10;
                    ParentWindow.Left = test.x - (_lastValidWidth / 2);
                }
                ParentWindow.WindowState = WindowState.Normal;
                ParentWindow.DragMove();
            }
        }

        private void CloseWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.Close();
        }

        private void MinimizeWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.WindowState = WindowState.Maximized;
        }

        private void RestoreWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.WindowState = WindowState.Normal;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ParentWindow.WindowState == WindowState.Normal)
                ParentWindow.WindowState = WindowState.Maximized;
            else
                ParentWindow.WindowState = WindowState.Normal;
        }
    }
}
