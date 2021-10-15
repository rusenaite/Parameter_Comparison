using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class Columns
    {
        public const int ColumnWidth = 40;

        public readonly string[] ColumnName = { "Status", "ID", "Source Value", "Target Value" };

        public override string ToString()
        {
            return ColumnName[0].PadRight(ColumnWidth) + ColumnName[1].PadRight(ColumnWidth) + 
                   ColumnName[2].PadRight(ColumnWidth) + ColumnName[3];
        }
    }
}
