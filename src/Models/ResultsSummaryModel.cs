using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;

namespace ParameterComparison.src.Models
{
    public class ResultsSummaryModel : IRequestModel
    {
        public List<ComparisonResultCount> ResultCount { get; set; }
        public List<ComparedParam> ComparedData { get; set; }
    }
}
