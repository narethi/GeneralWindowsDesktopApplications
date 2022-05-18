using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserAndReader.Models.Template
{
    public struct PropertyPatternEntry
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string AcceptableValues { get; set; }
        public string TrailingSeparator { get; set; }
    }
}
