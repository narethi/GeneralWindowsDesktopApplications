using System;

namespace LogParserAndReader.Exceptions
{
    /// <summary>
    /// The exception is for cases where a property entry template value has an unknown type
    /// </summary>
    internal class InvalidTemplatePropertyTypeException : Exception
    {
        /// <summary>
        /// This is the invalid type name
        /// </summary>
        public string PropertyType { get; set; } = string.Empty;

        /// <summary>
        /// This is the name of the template that has the invalid type
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Property {PropertyName} of type {PropertyType} is not valid {Environment.NewLine} {base.ToString()}";
        }
    }
}
