using System.Collections.Generic;

namespace LogParserAndReader.Models.LogFile
{
    /// <summary>
    /// This is used to store all of the data that is related to the log file
    /// </summary>
    public class LogFile
    {
        private readonly List<LogFileEntry> _logFileEntries = new List<LogFileEntry>();

        internal string _logFileName = string.Empty;
        public string LogFileName => _logFileName;

        /// <summary>
        /// Adds a new logfile entry to the list of logfile entries
        /// </summary>
        /// <param name="entry"></param>
        public void AddEntryToFile(LogFileEntry entry)
        {
            _logFileEntries.Add(entry);
        }

        /// <summary>
        /// One Way property that makes a shallow clone of the readonly list of entries in this LogFile
        /// </summary>
        public List<LogFileEntry> LogFileEntries => new List<LogFileEntry>(_logFileEntries);
    }
}
