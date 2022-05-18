using LogParserAndReader.Models.Template;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogParserAndReader.Parsers
{
    /// <summary>
    /// This reads the template strings and processes them to be used with the log parser
    /// </summary>
    public class FileTemplateParser
    {
        /* 
         * The pattern I want to support is:
         * <{PropertyName}:{PropertyType}:{PropertyValues}>{Separator}
         * * <> - Use XML style tags
         * * {PropertyName} is name of the column that this entry will be used in the output
         * * {PropertyType} is acceptable type of infomation for this column
         * * {PropertyValues} is the set of acceptable values for that type, this can be regex, csv, or enum. If blank this will accept any value of that type
         * * {Separator} is the character that is chosen to be the separator at the end of the tag, this can be a regex
         * Sample <DateTime:DateTime>{-}<ErrorLevel:String:(WARNING|ERROR|INFORMATION)>{-}<ThreadId:GUID>{ }<Message:String>
        */

        /// <summary>
        /// This is the pattern that is used to split the properties and their following separate
        /// </summary>
        /// <remarks>
        /// Form:
        /// <(.*?)> - This is looking for a string that starts and ends with exactly "<>" chars, and has any content
        /// {(.*?)} - This is looking for a string that starts and ends with exactly "{}" chars, and has any content
        /// <(.*?)>{(.*?)} - This entirely ensures that the string contains "<>" followed by "{}"
        /// </remarks>
        public const string RegexPatternForFileInformation = @"<(.*?)>{(.*?)}";

        /// <summary>
        /// This is the patter for separating the separator characters from the rest of the property character
        /// </summary>
        /// <remarks>
        /// (?!.*{) - This excludes the '{' char from what is allowed in the wild card
        /// ((?!.*{).*?) - This then selects anything in the list of everything except for '}'
        /// {((?!.*{).*?)} - This selects for a string that starts and ends with "{}" that does not include and internal "{}"
        /// {((?!.*{).*?)}$ - This looks for the last occurrence of the strings that ends with "{}" that does not include and internal "{}"
        /// </remarks>
        public const string RegexPatternForSeparator = @"{((?!.*{).*?)}$";

        private static readonly Regex entryPatternRegex = new Regex(RegexPatternForFileInformation, RegexOptions.Compiled);
        private static readonly Regex separatorPatternRegex = new Regex(RegexPatternForSeparator, RegexOptions.Compiled);

        /// <summary>
        /// This reads in a file, confirms its ready and then parses the template pattern from the file
        /// </summary>
        /// <param name="templateStringFileName"></param>
        /// <returns>True if the file exists and its read successfully</returns>
        public static Queue<PropertyPatternEntry> ConstructPatternListFromFile(string templateStringFileName)
        {
            if(File.Exists(templateStringFileName))
            {
                var fileContents = File.ReadAllText(templateStringFileName);
                return ConstructPatternList(fileContents);
            }
            return new();
        }

        /// <summary>
        /// This takes the remaining values of the accepted value string if they are broken into extra chunks
        /// </summary>
        /// <param listOfValues="">These are the values that need to be recombined</param>
        /// <returns>The recombined list of values with the ':' chars added back in</returns>
        private static string RecombineSeparatedAcceptedValueString(string[] listOfValues)
        {
            if (!listOfValues.Any()) return string.Empty;
            var recombinedValue = listOfValues[0];
            for(var i = 1; i < listOfValues.Length; i++)
            {
                recombinedValue += $":{listOfValues[i]}";
            }
            return recombinedValue;
        }

        private static T[] GetSubArray<T>(T[] value, int startIndex, int endIndex = -1)
        {
            var subArray = new T[((endIndex > 0) ? endIndex : value.Length) - startIndex];
            for(var  i = startIndex; i < ((endIndex > 0) ? endIndex : value.Length); i++)
            {
                subArray[i - startIndex] = value[i];
            }
            return subArray;
        }

        /// <summary>
        /// This reads the template string in, parses and stores the information about the logs being read
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>True if the input is parsed successfully</returns>
        public static Queue<PropertyPatternEntry> ConstructPatternList(string templateString)
        {
            var patternPropertyEntriesQueue = new Queue<PropertyPatternEntry>();
            /*
             * Look for the index of all '<' before a "}\s*<"
             * * This will be the naive pattern for the start and end
            */
            var matchedEntries = entryPatternRegex.Matches(templateString);
            foreach (var matchedEntry in matchedEntries)
            {
                var propertyTemplateString = matchedEntry.ToString();
                if (string.IsNullOrEmpty(propertyTemplateString)) continue;
                var propertyParts = separatorPatternRegex.Split(propertyTemplateString);
                //We need to ensure we have the property part and the trailing separator value, if not exit as the template is borked
                if (propertyParts.Length < 2) return new();

                var entryProperty = propertyParts[0].Trim('<').Trim('>');
                var propertyComponents = entryProperty.Split(':');
                var entry = new PropertyPatternEntry() 
                { 
                    TrailingSeparator = (propertyParts[1] == @"\n" || propertyParts[1] == @"\r\n") ? Environment.NewLine : propertyParts[1],
                    PropertyName = propertyComponents[0],
                    PropertyType = propertyComponents[1],
                    AcceptableValues = propertyComponents.Length < 3 ? propertyComponents[1] : RecombineSeparatedAcceptedValueString(GetSubArray(propertyComponents, 2)),
                };
                patternPropertyEntriesQueue.Enqueue(entry);
            }
            return patternPropertyEntriesQueue;
        }
    }
}
