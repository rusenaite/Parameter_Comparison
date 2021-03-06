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

        /// <summary>
        /// Method prints Columns class in format.
        /// </summary>
        /// <returns> Formated column names to print. </returns>
        public override string ToString()
        {
            return "\n\n" + ColumnName[0].PadRight(ColumnWidth / 2) + ColumnName[1].PadRight(ColumnWidth / 2) + 
                   ColumnName[2].PadRight(ColumnWidth) + ColumnName[3];
        }
    }
}
