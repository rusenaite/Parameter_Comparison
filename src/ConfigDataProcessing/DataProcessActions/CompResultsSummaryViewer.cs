﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.ConfigDataProcessing.DataProcessActions
{
    public class CompResultsSummaryViewer : ConfigurationComparison
    {
        /// <summary>
        /// Method allows to print comparison result summary.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public void ViewComparisonResultsSummary(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            List<ComparedParam> list = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            for (int i = 0; i < resultCount.Length; ++i)
            {
                resultCount[i].count = list.Where(pair => pair.Action == resultCount[i].result && pair.Action.Any()).Count();
            }

            PrintComparisonResultsSummary(resultCount);
        }

        /// <summary>
        /// Method prints summary of provided calculated comparison results.
        /// </summary>
        /// <param name="count"></param>
        public static void PrintComparisonResultsSummary((string result, int count)[] count)
        {
            count.Where(item => item.result != null && item.result.Any() && count != null).ToList().ForEach(tuple =>
            {
                Console.Write($"{tuple.result}:{tuple.count} ");
            });
        }

    }
}