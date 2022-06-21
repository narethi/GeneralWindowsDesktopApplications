using System;
using System.Text.RegularExpressions;

namespace LogParserAndReader
{
    /// <summary>
    /// This contains the list of public constants that can be used by template as log property types
    /// </summary>
    public static class PropertyTypeStringConstants
    {
        public const string DateTimeString = nameof(DateTime);
        public const string GuidString = nameof(Guid);
        public const string MessageString = "Message";
        public const string NumberString = "Number";
        public const string RegexString = nameof(Regex);
        public const string EnumString = nameof(Enum);
    }
}
