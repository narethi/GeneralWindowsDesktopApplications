using LogParserAndReader.Controllers;
using LogParserAndReader.Factories;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace LogParserAndReader.ViewModels
{
    /// <summary>
    /// This is the panel that contains all of the controls for filtering the log list
    /// </summary>
    internal class FilterControlPanelViewModel : BaseViewModel, IDisposable
    {
        private ApplicationConfigurations AppConfig => ApplicationConfigurations.Instance;
        private FilterController? _controller;
        public FilterController? Controller => _controller;

        public FilterControlPanelViewModel()
        {
            AppConfig.TemplateFileLoaded += Instance_TemplateFileLoaded;
        }

        internal void SetFilterController(FilterController controller)
        {
            _controller = controller;
        }

        private UIElement _filterControls = new();

        /// <summary>
        /// This is the content of the content control, this is
        /// constructed as a stack panel that contains all of the required
        /// filter controls (as per listed in the log template)
        /// </summary>
        public UIElement FilterControls
        {
            get => _filterControls;
            set
            {
                _filterControls = value;
                OnPropertyChanged();
            }
        }

        private void Instance_TemplateFileLoaded()
        {
            CreateFilterControl();
        }

        /// <summary>
        /// Iterate through the available template items add a filter option for each template item
        /// TODO: This is a target for assembly import loading such that custom dlls can be made to match custom patterns
        /// </summary>
        public void CreateFilterControl()
        {
            var panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            foreach (var templateEntry in AppConfig.LogFileTemplate)
            {
                panel.Children.Add(FilterControlFactory.ConstructControl(templateEntry));
            }
            
            //TODO: Update this to dispose the child controls, as this should always be a stack panel
            if(FilterControls is IDisposable disposableControl)
            {
                disposableControl.Dispose();
            }
            FilterControls = panel;
        }

        #region IDisposable Implementation

        private bool _isDisposed = false;

        #if DEBUG
                private string _created = Environment.StackTrace;
        #endif

        ~FilterControlPanelViewModel()
        {
            if (!_isDisposed)
            {
                Debug.Assert(false, "Failed to dispose the Filter Control");
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                //TODO: Update this to clean up the FilterControls
                AppConfig.TemplateFileLoaded -= Instance_TemplateFileLoaded;
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
