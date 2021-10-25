using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class FilteredParamViewer : ConfigurationComparison, IFilteringAction
    {
        /// <summary>
        /// Method prints filtered parameters by a given key value (filter).
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <param name="filter"></param>
        public void View(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string filter)
        {
            List<ComparedParam> list = CompareConfig(sourceData, targetData);
            List<ComparedParam> foundParamList = SearchForValue(list, filter);

            if (!foundParamList.Any())
            {
                Console.Error.WriteLine("\n\nCompared data does not contain parameters with entered ID value.");
            }
            else
            {
                ConfigurationDataPrinter.PrintComparedData(foundParamList);
            }
        }
    }
}
