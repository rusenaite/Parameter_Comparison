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
        /// Method creates parameter's list model - a list of compared parameters.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If creation went well, returns a list of compared parameters, otherwise - 
        /// throws an exception. </returns>
        public List<ComparedParam> GetResult(string choice)
        {
            List<ComparedParam> list = ConfigurationComparison.CompareConfig(SourceData, TargetData);
            List<ComparedParam> chosenList = new();

            if (Enum.TryParse(choice, out ParamAction action))
                chosenList = list.Where(pair => pair.Action == action).ToList();

            return chosenList.GetIntTypeKeys();
        }

    }
}
