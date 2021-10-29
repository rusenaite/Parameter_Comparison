using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;

namespace ParameterComparison.src.Models
{
    public class ComparisonResultFilterModel : IRequestModel
    {
        public List<ComparedParam> FilteredDataByComparisonResult { get; set; }
    }
}
