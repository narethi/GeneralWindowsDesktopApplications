using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.Parsers.PropertyParsers
{
    internal static class MessageStringParser
    {
        public static bool TryParse(string input, out string output)
        {
            output = input;
            return false;
        }

        public static string Parse(string input)
        {
            return input;
        }
    }
}
