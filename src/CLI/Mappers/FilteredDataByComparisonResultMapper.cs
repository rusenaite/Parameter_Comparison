using ParameterComparison.src.CLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return model.Create(choice);
        }

        public void Print(List<ComparedParam> result)
        {
            Printers.PrintComparedData(result);
        }
    }
}
