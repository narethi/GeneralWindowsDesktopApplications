using LogParserAndReader.Models.LogFile;
using LogParserAndReader.Models.Template;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.Parsers
{
    /// <summary>
    /// This is the class that is responsible to parsing a log file then applying the log file template
    /// </summary>
    public class LogFileParser
    {
        /// <summary>
        /// This 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static LogFile ReadLogsFromFile(string fileName)
        {
            if(File.Exists(fileName))
            {
                var fileContents = File.ReadAllText(fileName);
                return ReadLogData(fileContents);
            }
            return new LogFile();
        }

        private static string ProcessPropertyValue(string inputValue, string valueType)
        {
            return string.Empty;
        }

        private static LogFileProperty ReadNextProperty(PropertyPatternEntry propertyTemplate, string logsContent, out string processedLogContent)
        {
            var values = logsContent?.Split(propertyTemplate.TrailingSeparator, 2, StringSplitOptions.None);

            if (values?.Length > 1)
                processedLogContent = values[1];
            else
                processedLogContent = string.Empty;

            var stringToProcess = values?[0] ?? string.Empty;

            return new() { PropertyType = propertyTemplate.PropertyType, PropertyValue = stringToProcess };
        }

        public static LogFile ReadLogData(string fileContents)
        {
            /*
             * 1: Pull in the template information from the configurations
             * 2: Iterate over the string in the Queue and start deconstructing the log information
             * 3: Keep track of where in the file we are so we can read the delta later
             */
            var logFile = new LogFile();
            var contentSize = fileContents.Length;
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
