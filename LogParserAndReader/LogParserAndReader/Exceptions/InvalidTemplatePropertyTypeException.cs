using System;

namespace LogParserAndReader.Exceptions
{
    internal class InvalidTemplatePropertyTypeException : Exception
    {
        public string PropertyType { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Property {PropertyName} of type {PropertyType} is not valid {Environment.NewLine} {base.ToString()}";
        }
    }
}
