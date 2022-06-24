using System.Windows;
using System.Windows.Media;

namespace SimpleUIElements.Helpers
{
    /// <summary>
    /// All methods needed for navigating Visual and Logical tree controls
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// Recursively finds the first parent with type T
        /// </summary>
        /// <typeparam name="T">The type of the parent we are looking for</typeparam>
        /// <param name="childElement">This is the element we are looking for the parent of</param>
        /// <returns>the first parent of type T, or null if there is no parent with type T</returns>
        public static T? FindFirstParentOfType<T>(DependencyObject childElement) where T : DependencyObject
        {
            var parent = LogicalTreeHelper.GetParent(childElement);
            if (parent == null)
                parent = VisualTreeHelper.GetParent(childElement);

            if(parent == null)
            {
                return null;
            }

            if (parent.GetType() == typeof(T))
            {
                return (T)parent;
            }
            return FindFirstParentOfType<T>(parent);
        }
    }
}
