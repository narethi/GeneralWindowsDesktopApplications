using LogParserAndReader.Controllers;
using LogParserAndReader.Exceptions;
using LogParserAndReader.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace LogParserAndReader.Views
{
    /// <summary>
    /// Interaction logic for FilterControl.xaml
    /// 
    /// The filter control needs to do the following
    /// 1: Read the template information from that application configurations and make controls based on what is in the template
    /// 2: Update the Filter Controller such that when the filter control is used by the user the list filter will change as well
    /// 3: Layout the controls in a smart way such that they fit in the filter control in a nice way
    /// 
    /// I need to group some of the controls together
    /// - What grouping would make sense?
    /// - How would I title a group?
    /// - Would I want the grouping information to be added to the template?
    /// 
    /// </summary>
    public partial class FilterControl : UserControl, IDisposable
    {
        private readonly FilterControlViewModel _viewModel = new();
        public FilterControl()
        {
            DataContext = _viewModel;
            InitializeComponent();
            _viewModel.CreateFilterControl();
        }

        #region Dependency Properties

        public static readonly DependencyProperty FilterControllerProperty =
            DependencyProperty.Register(nameof(FilterController),
            typeof(FilterController),
            typeof(FilterControl), new FrameworkPropertyMetadata() { PropertyChangedCallback = OnDependencyPropertyChanged }
);

        /// <summary>
        /// This is the backing for the LogsControllerProperty
        /// </summary>
        public FilterController? FilterController
        {
            get => _viewModel.Controller;
            set => SetValue(FilterControllerProperty, value);
        }

        #endregion

        private static void OnDependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FilterControl control)
            {
                if (e.Property.Name.Equals(nameof(FilterController)))
                {
                    control._viewModel.SetFilterController((FilterController)e.NewValue);
                }
            }
        }

        #region IDisposable Implementation

        private bool _isDisposed = false;
        
        #if DEBUG
            private string _created = Environment.StackTrace;
        #endif

        ~FilterControl()
        {
            if (!_isDisposed)
            {
                Debug.Assert(false, "Failed to dispose the Filter Control");
            }
        }

        private void Dispose(bool disposing)
        {
            if(disposing)
            {
                _viewModel.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
