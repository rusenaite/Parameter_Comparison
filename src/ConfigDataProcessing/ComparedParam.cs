using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ComparedParam
    {
        public KeyValuePair<int, string> SourcePair { get; set; }
        public KeyValuePair<int, string> TargetPair { get; set; }
        public string Action { get; set; }
        public ConsoleColor Color { get; set; }
    }
}
