using LogParserAndReader.Exceptions;
using LogParserAndReader.Models.Template;
using LogParserAndReader.Views.FilterControls;

namespace LogParserAndReader.Factories
{
    internal class FilterControlFactory
    {
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
