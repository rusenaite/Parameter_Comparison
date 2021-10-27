using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.Models
{
    public class FilteredDataByIdModel
    {
        public static Dictionary<string, string> SourceData;
        public static Dictionary<string, string> TargetData;

        public FilteredDataByIdModel(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
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
        public List<ComparedParam> Create(string id)
        {
            List<ComparedParam> list = ConfigurationComparison.CompareConfig(SourceData, TargetData);
            List<ComparedParam> results = ConfigurationComparison.SearchForValue(list, id);

            if (!results.Any())
            {
                throw new NullReferenceException();
            }
            else
            {
                return results;
            }
        }
    }
}
