using System;
using System.Windows;

namespace SimpleUIElements.Views
{
    public class CustomCaptionWindow : Window
    {
        protected UIElement _captionBarControl;
        public UIElement CaptionBarControl => _captionBarControl;

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
    }
}
