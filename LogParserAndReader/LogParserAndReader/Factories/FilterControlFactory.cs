using LogParserAndReader.Exceptions;
using LogParserAndReader.Models.Template;
using LogParserAndReader.Views.FilterControls;

namespace LogParserAndReader.Factories
{
    internal class FilterControlFactory
    {
        /// <summary>
        /// Takes a pattern input and constructs a filter control that can be used with it
        /// TODO: Update the factory to be able to use external controls
        /// </summary>
        /// <param name="filterPattern">This is the pattern that the filter control will be made from</param>
        /// <returns>A new control that matches the filter pattern type</returns>
        /// <exception cref="InvalidTemplatePropertyTypeException">Throws if an unknown property type is provided</exception>
        internal static BaseFilterControl ConstructControl(PropertyPatternEntry filterPattern)
        {
            switch (filterPattern.PropertyType)
            {
                case PropertyTypeStringConstants.DateTimeString:
                    return new StringFilterControl() { ControlName = filterPattern.PropertyName};
                case PropertyTypeStringConstants.GuidString:
                    return new StringFilterControl() { ControlName = filterPattern.PropertyName };
                case PropertyTypeStringConstants.MessageString:
                    return new StringFilterControl() { ControlName = filterPattern.PropertyName };
                case PropertyTypeStringConstants.NumberString:
                    return new StringFilterControl() { ControlName = filterPattern.PropertyName };
                case PropertyTypeStringConstants.RegexString:
                    return new StringFilterControl() { ControlName = filterPattern.PropertyName };
                case PropertyTypeStringConstants.EnumString:
                    return new StringFilterControl() { ControlName = filterPattern.PropertyName };
                default:
                    throw new InvalidTemplatePropertyTypeException() { PropertyType = filterPattern.PropertyType, PropertyName = filterPattern.PropertyName };
            }
        }
    }
}
