using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.Parsers.PropertyParsers
{
    /// <summary>
    /// Parses all number strings into formatted n
    /// </summary>
    internal static class NumberStringParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameters"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        internal static bool TryParse(string input, string parameters, out double output)
        {
            output = 1;
            return false;
        }

        internal static double Parse(string input, string parameters)
        {
            return double.Parse(input);
        }
    }
}
