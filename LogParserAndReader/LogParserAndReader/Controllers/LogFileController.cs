using LogParserAndReader.Models.LogFile;
using LogParserAndReader.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.Controllers
{
    public class LogFileController
    {
        private LogFile _loadedFile;
        public void ReadFile(string fileName)
        {
            _loadedFile = LogFileParser.ReadLogsFromFile(fileName);
        }
    }
}
