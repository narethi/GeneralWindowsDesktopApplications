using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.Models.LogFile
{
    /// <summary>
    /// This is used to store all of the data that is related to the log file
    /// </summary>
    public class LogFile
    {
        private readonly List<LogFileEntry> _logFileEntries = new List<LogFileEntry>();
        public void AddEntryToFile(LogFileEntry entry)
        {
            _logFileEntries.Add(entry);
        }

        public List<LogFileEntry> LogFileEntries => new List<LogFileEntry>(_logFileEntries);
    }
}
