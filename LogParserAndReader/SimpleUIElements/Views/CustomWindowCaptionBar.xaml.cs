using SimpleUIElements.ViewModels;
using System;
using System.Runtime.InteropServices;
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
        private readonly CustomWindowCaptionBarViewModel _viewModel = new();
        public CustomWindowCaptionBar()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void CustomWindowCaptionBar_StateChanged(object? sender, EventArgs e)
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

        private void CustomWindowCaptionBar_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).StateChanged += CustomWindowCaptionBar_StateChanged;
        }

        private void CustomWindowCaptionBar_UnLoaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).StateChanged -= CustomWindowCaptionBar_StateChanged;
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if(e.RightButton != MouseButtonState.Pressed)
            {
                Window.GetWindow(this).WindowState = WindowState.Normal;
                Window.GetWindow(this).DragMove();
            }
        }

        private void CloseWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void MinimizeWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void MaximizeWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Maximized;
        }

        private void RestoreWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Normal;
        }
    }
}
