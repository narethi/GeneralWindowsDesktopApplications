using LogParserAndReader.Models.Template;
using LogParserAndReader.Parsers;
using System.Collections.Generic;
using System.Linq;
using static LogParserAndReader.Controllers.LogFileController;

namespace LogParserAndReader
{
    internal class ApplicationConfigurations
    {
        public event VoidHandler? TemplateFileLoaded;

        #region Singleton Implementation
        
        private static ApplicationConfigurations? _instance = null;
        private ApplicationConfigurations() { }
        public static ApplicationConfigurations Instance => _instance == null ? _instance = new ApplicationConfigurations() : _instance;

        #endregion

        /// <summary>
        /// This is a queue of the patterns that the template string being parsed is matched against
        /// The entries in this queue store the order that the properties in each entry row is expected to be 
        /// in the entry row
        /// </summary>
        private Queue<PropertyPatternEntry> _patternPropertyEntriesQueue = new Queue<PropertyPatternEntry>();

        /// <summary>
        /// This is used to provide access to the template queue, generate a new instance of it so users can't break the order (the template is queued in order)
        /// </summary>
        public Queue<PropertyPatternEntry> LogFileTemplate => new Queue<PropertyPatternEntry>(_patternPropertyEntriesQueue); 

        /// <summary>
        /// This will take a file that contains a log file template and then constructs a template instance to be used to parse a log file
        /// </summary>
        /// <param name="fileName">This is the name of the file that contains the </param>
        /// <returns>True if the file contains a valid log file template</returns>
        public bool ReadLogFileTemplateFromFile(string fileName)
        {
            _patternPropertyEntriesQueue = FileTemplateParser.ConstructPatternListFromFile(fileName);
            if(TemplateFileLoaded != null)
            {
                TemplateFileLoaded.Invoke();
            }
            return _patternPropertyEntriesQueue.Any();
        }
    }
}
