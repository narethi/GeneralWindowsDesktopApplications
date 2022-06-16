using SimpleUIElements.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUIElements.Views
{
    /// <summary>
    /// Interaction logic for WindowSizeToolBar.xaml
    /// </summary>
    public partial class WindowSizeToolBar : UserControl
    {
        private readonly WindowSizeToolBarViewModel _viewModel = new();

        public WindowSizeToolBar()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void WindowSizeToolBar_StateChanged(object? sender, EventArgs e)
        {
            var newState = Window.GetWindow(this).WindowState;
            if (newState == WindowState.Maximized)
            {
                _viewModel.IsWindowMaximized = true;
            }
            else
            {
                _viewModel.IsWindowMaximized = false;
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void RestoreWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Normal;
            _viewModel.IsWindowMaximized = false;
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Maximized;
            _viewModel.IsWindowMaximized = true;
        }

        private void WindowSizeBar_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).StateChanged += WindowSizeToolBar_StateChanged;
        }

        private void WindowSizeBar_UnLoaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).StateChanged -= WindowSizeToolBar_StateChanged;
        }
    }
}
