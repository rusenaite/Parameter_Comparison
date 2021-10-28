using ParameterComparison.src.ConfigDataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParameterComparison.src.CLI.Models
{
    public class FilteredDataByComparisonResultModel : IRequestModel
    {
        public static Dictionary<string, string> SourceData;
        public static Dictionary<string, string> TargetData;

        public FilteredDataByComparisonResultModel(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            SourceData = sourceData;
            TargetData = targetData;
        }

        /// <summary>
        /// Method creates filtered data by comparison result model - a list of compared parameters.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If creation went well, returns a list of filtered parameters, otherwise - an
        /// empty list. </returns>
        public List<ComparedParam> GetResult(string choice)
        {
            List<ComparedParam> list = ConfigurationComparison.CompareConfig(SourceData, TargetData);

            if (int.TryParse(choice, out int action))
            {
                return list.Where(pair => (int)pair.Action == action).ToList().GetIntTypeKeys();
            }

            return null;
        }

    }
}
