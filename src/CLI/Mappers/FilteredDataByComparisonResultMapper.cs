using ParameterComparison.src.CLI.Models;
using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;

namespace ParameterComparison.src.CLI.Mappers
{
    public class FilteredDataByComparisonResultMapper
    {
        /// <summary>
        /// Method creates passed model and returns result list.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> If creation went well, returns a list of parameters, otherwise - an empty list. </returns>
        public List<ComparedParam> Map(FilteredDataByComparisonResultModel model, string choice)
        {
            return model.GetResult(choice);
        }

        /// <summary>
        /// Method prints a list of filtered compared parameters.
        /// </summary>
        /// <param name="result"></param>
        public void Print(List<ComparedParam> result)
        {
            Printers.PrintComparedData(result);
        }
    }
}
