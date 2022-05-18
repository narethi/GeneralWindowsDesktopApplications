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
        private readonly List<LogFileEntry> logFileEntries = new List<LogFileEntry>();
        public void AddEntryToFile(LogFileEntry entry)
        {
            logFileEntries.Add(entry);
        }
    }
}
