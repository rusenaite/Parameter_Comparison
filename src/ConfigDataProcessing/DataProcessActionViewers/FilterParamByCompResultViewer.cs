﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class FilterParamByCompResultViewer : ConfigurationComparison, IFilteringAction
    {
        /// <summary>
        /// Method allows printing parameters of selected comparison result.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <param name="choice"></param>
        public void View(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string choice)
        {
            List<ComparedParam> list = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());
            List<ComparedParam> chosenList = new();

            if (Enum.TryParse<ParamAction>(choice, out ParamAction action))
                chosenList = list.Where(pair => pair.Action == action).ToList();

            if (!chosenList.Any())
            {
                Console.Error.WriteLine("\n\nCompared data does not contain parameters with selected comparison result.");
            }
            else
            {
                ConfigurationDataPrinter.PrintComparedData(chosenList);
            }
        }
    }
}
