using LogParserAndReader.Controllers;
using LogParserAndReader.Exceptions;
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
                switch (templateEntry.PropertyType)
                {
                    case PropertyTypeStringConstants.DateTimeString:
                        panel.Children.Add(new TextBlock() { Text = "This is the date time control" });
                        break;
                    case PropertyTypeStringConstants.GuidString:
                        panel.Children.Add(new TextBlock() { Text = "This is the GUID Filter control" });
                        break;
                    case PropertyTypeStringConstants.MessageString:
                        panel.Children.Add(new TextBlock() { Text = "This is the Message string Filter control" });
                        break;
                    case PropertyTypeStringConstants.NumberString:
                        panel.Children.Add(new TextBlock() { Text = "This is the Message string Filter control" });
                        break;
                    case PropertyTypeStringConstants.RegexString:
                        panel.Children.Add(new TextBlock() { Text = "This is the Regex string Filter control" });
                        break;
                    case PropertyTypeStringConstants.EnumString:
                        panel.Children.Add(new TextBlock() { Text = "This is the Enum string Filter control" });
                        break;
                    default:
                        throw new InvalidTemplatePropertyTypeException() { PropertyType = templateEntry.PropertyType, PropertyName = templateEntry.PropertyName };
                }
            }
            
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
