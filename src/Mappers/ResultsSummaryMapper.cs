using ParameterComparison.src.Models;
using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;
using System.Linq;

namespace ParameterComparison.src.CLI.Mappers
{
    public class ResultsSummaryMapper
    {
        public Dictionary<string, string> SourceData;
        public Dictionary<string, string> TargetData;

        public ResultsSummaryMapper(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            SourceData = sourceData;
            TargetData = targetData;
        }

        public ResultsSummaryModel Map(ResultsSummaryModel model)
        {
            model.ComparedData = ConfigurationComparison.CompareConfig(SourceData, TargetData);

            for (int i = 0; i < ConfigurationComparison.resultCount.Capacity; ++i)
            {
                ConfigurationComparison.resultCount[i].Count = model.ComparedData.Where(pair => pair.Action == ConfigurationComparison.resultCount[i].Result).Count();
            }

            model.ResultCount = ConfigurationComparison.resultCount.ToList();

            return model;
        }
    }
}
