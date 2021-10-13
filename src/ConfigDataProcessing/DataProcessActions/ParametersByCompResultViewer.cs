﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.ConfigDataProcessing.DataProcessActions
{
    public class ParametersByCompResultViewer : ConfigurationComparison
    {
        /// <summary>
        /// Method allows printing parameters of selected comparison result.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <param name="choice"></param>
        public void ViewParamByComparisonResult(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string choice)
        {
            List<ComparedParam> list = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            List<ComparedParam> chosenList = list.Where(pair => pair.Action == choice && pair.Action.Any()).ToList();

            if (!chosenList.Any())
            {
                Console.Error.WriteLine("\n\nCompared data does not contain parameters with selected comparison result.");
            }
            else
            {
                ConfigurationDataPrinter.PrintColumnNames();
                ConfigurationDataPrinter.PrintComparedData(chosenList);
            }
        }
    }
}