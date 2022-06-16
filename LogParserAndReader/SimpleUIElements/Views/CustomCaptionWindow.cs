using System;
using System.Windows;

namespace SimpleUIElements.Views
{
    /// <summary>
    /// This is the abstract base class for the Custom Caption Window, 
    /// this window is based on the CustomCaptionWindow style in WindowResources.xaml
    /// </summary>
    public abstract class CustomCaptionWindow : Window
    {
        protected UIElement _captionBarControl;
        
        /// <summary>
        /// This is a one way property for the custom control in the caption bar. This can be file menus, search bars, tabs, etc.
        /// </summary>
        public UIElement CaptionBarControl => _captionBarControl;

        /// <summary>
        /// This is the basic constructor that will set the window style, and window style
        /// </summary>
        /// <param name="captionBarControl">This is control that will be injected into the caption bar</param>
        public CustomCaptionWindow(UIElement? captionBarControl = null)
        {
            var myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source =
                new Uri("/SimpleUIElements;component/WindowResources.xaml",
                        UriKind.RelativeOrAbsolute);
            var windowStyle = myResourceDictionary[nameof(CustomCaptionWindow)];
            if (windowStyle != null)
                Style = (Style)windowStyle;

            _captionBarControl = captionBarControl ?? new();
        }

        /// <summary>
        /// Hide the default constructor
        /// </summary>
        private CustomCaptionWindow() { _captionBarControl = new UIElement(); }
    }
}
