using ParameterComparison.src.Models;
using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;

namespace ParameterComparison.src.CLI.Mappers
{
    public class FilteredDataByIdMapper
    {
        public Dictionary<string, string> SourceData;
        public Dictionary<string, string> TargetData;

        public FilteredDataByIdMapper(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            SourceData = sourceData;
            TargetData = targetData;
        }

        public IdFilterModel Map(IdFilterModel model, string id)
        {
            List<ComparedParam> list = ConfigurationComparison.CompareConfig(SourceData, TargetData);
            model.FilteredDataById = ConfigurationComparison.SearchForValue(list, id).GetIntTypeKeys();
            return model;
        }
    }
}
