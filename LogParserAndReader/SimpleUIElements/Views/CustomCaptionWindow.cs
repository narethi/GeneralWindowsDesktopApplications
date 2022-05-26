using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleUIElements.Views
{
    public class CustomCaptionWindow : Window
    {
        public CustomCaptionWindow()
        {
            var myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source =
                new Uri("/SimpleUIElements;component/WindowResources.xaml",
                        UriKind.RelativeOrAbsolute);
            var windowStyle = myResourceDictionary[nameof(CustomCaptionWindow)];
            if (windowStyle != null)
                Style = (Style)windowStyle;
        }
    }
}
