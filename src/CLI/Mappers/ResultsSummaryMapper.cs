using ParameterComparison.src.CLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.Mappers
{
    public class ResultsSummaryMapper
    {
        /// <summary>
        /// Method creates passed model and returns result.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> If creation went well, returns an array of results, otherwise - an empty array. </returns>
        public (ParamAction, int)[] Map(ResultsSummaryModel model)
        {
            return model.Create();
        }

        public void Print((ParamAction, int)[] result)
        {
            Printers.PrintComparisonResultsSummary(result);
        }
    }
}
