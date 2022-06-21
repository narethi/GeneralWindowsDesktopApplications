using System;
using System.Text.RegularExpressions;

namespace LogParserAndReader.Parsers.PropertyParsers
{
    internal static class EnumParser
    {
        /// <summary>
        /// This is the patter for separating the separator characters from the rest of the property character
        /// </summary>
        /// <remarks>
        /// (?!.*{) - This excludes the '{' char from what is allowed in the wild card
        /// ((?!.*{).*?) - This then selects anything in the list of everything except for '}'
        /// {((?!.*{).*?)} - This selects for a string that starts and ends with "{}" that does not include and internal "{}"
        /// {((?!.*{).*?)}$ - This looks for the last occurrence of the strings that ends with "{}" that does not include and internal "{}"
        /// </remarks>
        public const string RegexPatternForEnumType = @"^{((?!.*{).*?)}";

        private static readonly Regex enumTypePatternRegex = new Regex(RegexPatternForEnumType, RegexOptions.Compiled);

        public static Enum Parse(string inputType, string inputValue)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(string inputType, string inputValue, out Enum? parsedValue)
        {
            /*
             * 1: parse the input type to get the type object and acceptable values
             * 2: using reflection find the type information
             * 3: using reflection find the value for the input value from the input type
             */
            var matchedEntries = enumTypePatternRegex.Matches(inputType);
            if (matchedEntries.Count != 0)
            {
                var type = Type.GetType(matchedEntries[0].Value.Trim('{').Trim('}')); 
                if(type != null)
                {
                    if(Enum.TryParse(type, inputValue, out var parsedEnum) && parsedEnum != null)
                    {
                        parsedValue = (Enum)parsedEnum;
                        return true;
                    }
                }
            }
            parsedValue = default(Enum);
            return false;
        }
    }
}
