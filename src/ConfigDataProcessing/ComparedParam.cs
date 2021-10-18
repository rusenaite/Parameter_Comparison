using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ComparedParam
    {
        private readonly int ColumnWidth = 40;

        public KeyValuePair<int, string> SourcePair { get; set; }
        public KeyValuePair<int, string> TargetPair { get; set; }
        public string Action { get; set; }

        public override string ToString()
        {
            return Action.PadRight(ColumnWidth / 2) + SourcePair.Key.ToString().PadRight(ColumnWidth / 2) + SourcePair.Value.ToString().PadRight(ColumnWidth) + TargetPair.Value.ToString().PadRight(ColumnWidth);
        }
    }
}
