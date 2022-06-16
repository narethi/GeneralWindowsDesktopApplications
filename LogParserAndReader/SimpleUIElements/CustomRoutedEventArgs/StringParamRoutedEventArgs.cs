using System.Windows;

namespace SimpleUIElements.CustomRoutedEventArgs
{
    /// <summary>
    /// Routed event args for events that need a single string
    /// </summary>
    public sealed class StringParamRoutedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// This is the string value property that carries the data for the routed event 
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Public constructor required to construct these event args
        /// </summary>
        /// <param name="routedEvent">This is the event for these params</param>
        /// <param name="value">This is the value</param>
        public StringParamRoutedEventArgs(RoutedEvent routedEvent, string value) : base(routedEvent)
        {
            Value = value;
        }

        /// <summary>
        /// Hidden constructor, these args require a routed event default constructor is not allowed
        /// </summary>
        private StringParamRoutedEventArgs() { }

    }
}
