using LogParserAndReader.Controllers;
using LogParserAndReader.Exceptions;
using LogParserAndReader.Factories;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace LogParserAndReader.ViewModels
{
    internal class FilterControlViewModel : BaseViewModel, IDisposable
    {
        private ApplicationConfigurations AppConfig => ApplicationConfigurations.Instance;
        private FilterController? _controller;
        public FilterController? Controller => _controller;

        public FilterControlViewModel()
        {
            AppConfig.TemplateFileLoaded += Instance_TemplateFileLoaded;
        }

        internal void SetFilterController(FilterController controller)
        {
            _controller = controller;
        }

        private UIElement _filterControls = new();

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

        ~FilterControlViewModel()
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
