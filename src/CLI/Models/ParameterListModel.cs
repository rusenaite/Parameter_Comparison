using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.Models
{
    public class ParameterListModel : IRequestModel
    {
        public static Dictionary<string, string> SourceData;
        public static Dictionary<string, string> TargetData;

        public ParameterListModel(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
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
        public List<ComparedParam> Create()
        {
            List<ComparedParam> comparedData = ConfigurationComparison.CompareConfig(SourceData, TargetData);

            if (!comparedData.Any())
            {
                throw new NullReferenceException();
            }
            else
            {
                return comparedData;
            }
        }
    }
}
