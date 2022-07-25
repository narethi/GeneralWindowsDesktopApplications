using SimpleUIElements.Delegates;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUIElements.Views.UserControls.DelegateControls
{
    /// <summary>
    /// Interaction logic for DelegateButton.xaml
    /// </summary>
    public partial class DelegateButton : UserControl
    {
        public DelegateButton()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        public static readonly DependencyProperty ClickDelegateProperty =
            DependencyProperty.Register(nameof(Click),
            typeof(VoidDelegate),
            typeof(DelegateButton), new FrameworkPropertyMetadata()
        );

        /// <summary>
        /// This is the backing for the ClickDelegateProperty
        /// </summary>
        public VoidDelegate Click
        {
            get => (VoidDelegate)GetValue(ClickDelegateProperty);
            set => SetValue(ClickDelegateProperty, value);
        }

        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register(nameof(ButtonContent),
            typeof(object),
            typeof(DelegateButton), new FrameworkPropertyMetadata()
);

        /// <summary>
        /// This is the backing for the ClickDelegateProperty
        /// </summary>
        public object ButtonContent
        {
            get => GetValue(ButtonContentProperty);
            set => SetValue(ButtonContentProperty, value);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
                Click();
        }
    }
}
