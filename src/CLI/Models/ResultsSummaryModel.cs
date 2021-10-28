using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;
using System.Linq;

namespace ParameterComparison.src.CLI.Models
{
    public class ResultsSummaryModel : ConfigurationComparison, IRequestModel
    {
        public static Dictionary<string, string> SourceData;
        public static Dictionary<string, string> TargetData;

        public ResultsSummaryModel(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            SourceData = sourceData;
            TargetData = targetData;
        }

        /// <summary>
        /// Method creates results summary model - a list of compared parameters.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If creation went well, returns a list of compared parameters, otherwise - 
        /// throws an exception. </returns>
        public (ParamAction, int)[] GetResult()
        {
            List<ComparedParam> list = CompareConfig(SourceData, TargetData);

            for (int i = 0; i < resultCount.Length; ++i)
            {
                resultCount[i].count = list.Where(pair => pair.Action == resultCount[i].result).Count();
            }

            return resultCount;
        }
    }
}
