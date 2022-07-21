using System;

namespace LogParserAndReader.Exceptions
{
    internal class InvalidTemplatePropertyTypeException : Exception
    {
        public string PropertyType { get; set; }
        public string PropertyName { get; set; }

        public override string ToString()
        {
            return $"Property {PropertyName} of type {PropertyType} is not valid {Environment.NewLine} {base.ToString()}";
        }
    }
}
