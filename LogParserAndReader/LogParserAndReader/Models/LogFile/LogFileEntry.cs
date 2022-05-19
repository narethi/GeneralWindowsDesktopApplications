using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.Models.LogFile
{
    public class LogFileEntry
    {
        private readonly List<LogFileProperty> _logFileProperties = new List<LogFileProperty>();
        public void AddEntryInformation(LogFileProperty property)
        {
            _logFileProperties.Add(property);
        }

        public List<LogFileProperty> LogFileProperties => new List<LogFileProperty>(_logFileProperties);
    }
}
