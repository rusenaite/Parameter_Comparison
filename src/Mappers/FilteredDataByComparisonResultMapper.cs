using ParameterComparison.src.Models;
using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ParameterComparison.src.CLI.Mappers
{
    public class FilteredDataByComparisonResultMapper
    {
        public Dictionary<string, string> SourceData;
        public Dictionary<string, string> TargetData;

        public FilteredDataByComparisonResultMapper(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            SourceData = sourceData;
            TargetData = targetData;
        }

        public ComparisonResultFilterModel Map(ComparisonResultFilterModel model, string choice)
        {
            model.FilteredDataByComparisonResult = ConfigurationComparison.CompareConfig(SourceData, TargetData);

            if (int.TryParse(choice, out int action))
            {
                model.FilteredDataByComparisonResult = model.FilteredDataByComparisonResult.Where(pair => (int)pair.Action == action).ToList();
            }

            return model;
        }
    }
}
