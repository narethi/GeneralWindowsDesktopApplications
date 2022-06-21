using LogParserAndReader.Models.LogFile;
using LogParserAndReader.Models.Template;
using LogParserAndReader.Parsers.PropertyParsers;
using System;
using System.IO;
using System.Linq;

namespace LogParserAndReader.Parsers
{
    /// <summary>
    /// This is the class that is responsible to parsing a log file then applying the log file template
    /// </summary>
    public class LogFileParser
    {
        /// <summary>
        /// This takes a log file name then parses the file into a LogFile object that is used to populate the log control
        /// </summary>
        /// <param name="fileName">The name of the log file being opened</param>
        /// <returns>A fully parsed log file</returns>
        public static LogFile ReadLogsFromFile(string fileName)
        {
            if(File.Exists(fileName))
            {
                var fileContents = File.ReadAllText(fileName);
                var logfile = ReadLogData(fileContents);
                logfile._logFileName = Path.GetFullPath(fileName);
                return logfile;
            }
            return new LogFile();
        }

        /// <summary>
        /// Takes a value string and property type and processes the value string into a usable object based on type
        /// </summary>
        /// <param name="inputValue">value that is to be parsed</param>
        /// <param name="valueType">type that the value should be parsed into</param>
        /// <param name="modifiers">the list of string modifiers for the parse operation</param>
        /// <returns>The constructed object</returns>
        private static object ProcessPropertyValue(string inputValue, string valueType, string modifiers)
        {
            switch(valueType)
            {
                case PropertyTypeStringConstants.DateTimeString:
                    if(DateTime.TryParse(inputValue, out var dateTime))
                        return dateTime;
                    break;
                case PropertyTypeStringConstants.GuidString:
                    if(Guid.TryParse(inputValue, out var guid))
                        return guid;
                    break;
                case PropertyTypeStringConstants.MessageString:
                    return inputValue;
                case PropertyTypeStringConstants.NumberString:
                    return 2;
                case PropertyTypeStringConstants.RegexString:
                    return 0;
                case PropertyTypeStringConstants.EnumString:
                    if(EnumParser.TryParse(modifiers, inputValue, out var value) && value != null)
                    {
                        return value;
                    }
                    return 3;
            }
            //If the value can't be parsed throw the mismatch exception
            //throw new TemplateValueTypeMismatchException();
            return "#ERROR";
        }

        /// <summary>
        /// This attempts to read the next property in a log entry
        /// </summary>
        /// <param name="propertyTemplate"></param>
        /// <param name="logsContent"></param>
        /// <param name="processedLogContent"></param>
        /// <returns></returns>
        private static LogFileProperty ReadNextProperty(PropertyPatternEntry propertyTemplate, string logsContent, out string processedLogContent)
        {
            /*
             * MessageString and RegexString are special types where we need a look ahead to be added to this parser
             */ 
            var values = logsContent?.Split(propertyTemplate.TrailingSeparator, 2, StringSplitOptions.None);

            if (values?.Length > 1)
                processedLogContent = values[1];
            else
                processedLogContent = string.Empty;

            var stringToProcess = ProcessPropertyValue(values?[0] ?? string.Empty, propertyTemplate.PropertyType, propertyTemplate.AcceptableValues);

            return new() { PropertyType = propertyTemplate.PropertyType, PropertyValue = stringToProcess, PropertyName =  propertyTemplate.PropertyName};
        }

        /// <summary>
        /// Parse log file contents into a usable Logfile object
        /// </summary>
        /// <param name="fileContents">This is the log file content to read</param>
        /// <returns>log file object containing parsed log file information</returns>
        public static LogFile ReadLogData(string fileContents)
        {
            /*
             * 1: Pull in the template information from the configurations
             * 2: Iterate over the string in the Queue and start deconstructing the log information
             * 3: Keep track of where in the file we are so we can read the delta later
             */
            var logFile = new LogFile();
            var workingData = new string(fileContents);
            while(workingData.Any())
            {
                var logEntry = new LogFileEntry();
                var template = ApplicationConfigurations.Instance.LogFileTemplate;
                foreach (var entry in template)
                {
                    logEntry.AddEntryInformation(ReadNextProperty(entry, workingData, out workingData));
                }
                logFile.AddEntryToFile(logEntry);
            }
            return logFile;
        }
    }
}
