using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.ConfigDataProcessing
{
    public class ComparisonResultCount
    {
        public ComparisonResult Result { get; set; }
        public int Count { get; set; }

        public ComparisonResultCount(ComparisonResult result, int count)
        {
            Result = result;
            Count = count;
        }
    }
}
