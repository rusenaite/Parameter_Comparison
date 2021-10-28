using ParameterComparison.src.CLI.Models;
using ParameterComparison.src.ConfigDataProcessing;

namespace ParameterComparison.src.CLI.Mappers
{
    public class ResultsSummaryMapper
    {
        /// <summary>
        /// Method creates passed model and returns result.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> If creation went well, returns an array of results, otherwise - an empty array. </returns>
        public (ComparisonResult, int)[] Map(ResultsSummaryModel model)
        {
            return model.GetResult();
        }

        /// <summary>
        /// Method prints parameter comparison results.
        /// </summary>
        /// <param name="result"></param>
        public void Print((ComparisonResult, int)[] result)
        {
            Printers.PrintComparisonResultsSummary(result);
        }
    }
}
