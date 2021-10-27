﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParameterComparison
{
    public class ConfigurationComparison
    {

        public (ParamAction result, int count)[] resultCount = new[] { (ParamAction.U, 0), (ParamAction.M, 0), (ParamAction.R, 0), (ParamAction.A, 0) };

        /// <summary>
        /// Method compares integer-key-type source and target data and based on comparison
        /// result, puts results into a list of ComparedParam objects.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <returns> If comparison went well, returns a list of compared parameters, 
        /// otherwise - returns an empty list. </returns>
        public static List<ComparedParam> CompareConfig(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            var resultsList = new List<ComparedParam>();

            foreach (var srcPair in sourceData)
            {
                ComparePair(ref targetData, srcPair, ref resultsList);
            }

            foreach(var trgPair in targetData)
            {
                KeyValuePair<string, string> srcPair = default;

                var addedParam = new ComparedParam(srcPair, trgPair)
                {
                    Action = ParamAction.A
                };

                resultsList.Add(addedParam);
            }

            return resultsList.OrderBy(parameter => parameter.TargetPair.Key).ThenBy(parameter => parameter.SourcePair.Key).ToList();
        }

        /// <summary>
        /// Method seaches for source pairs in target data and, based on result, accordingly creates ComparedParam object.
        /// </summary>
        /// <param name="targetData"></param>
        /// <param name="srcPair"></param>
        /// <param name="resultsList"></param>
        public static void ComparePair(ref Dictionary<string, string> targetData, KeyValuePair<string, string> srcPair, ref List<ComparedParam> resultsList)
        {
            var trgKey = targetData.FirstOrDefault(x => x.Key == srcPair.Key).Key;

            if (trgKey != default)
            {
                var trgPair = targetData.FirstOrDefault(pair => pair.Key == trgKey);

                ComparedParam comparedParam = new ComparedParam(srcPair, trgPair);

                targetData.Remove(trgPair.Key);
                resultsList.Add(comparedParam);
            }
            else
            {
                KeyValuePair<string, string> trgPair = default;

                ComparedParam removedParam = new ComparedParam(srcPair, trgPair)
                {
                    Action = ParamAction.R
                };

                resultsList.Add(removedParam);
            }
        }
        
        /// <summary>
        /// Method checks whether the passed dictionary contains specific keys.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keys"></param>
        /// <returns> If dictionary contains specified keys - returns true, otherwise - 
        /// returns false. </returns>
        public static bool ContainsKeys(Dictionary<string, string> data, string[] keys)
        {
            return keys.Any() && keys.All(data.ContainsKey);
        }

        /// <summary>
        /// Method searches of a specific key value in a list of keys.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        /// <returns> If search went well, returns a data list that contain required key values, 
        /// otherwise - an empty list. </returns>
        public static List<ComparedParam> SearchForValue(List<ComparedParam> data, string value)
        {
            List<string> keysAsStrings = data.GetKeys();
            List<string> foundKeyList = new();

            bool found;

            foreach (var key in keysAsStrings)
            {
                found = key.StartsWith(value, false, CultureInfo.InvariantCulture);

                if (!found)
                {
                    continue;
                }

                foundKeyList.Add(key);
            }

            List<ComparedParam> foundPairs = FindDataByKeys(data, foundKeyList);

            return foundPairs;
        }

        /// <summary>
        /// Method searches for parameter data that contains keys from provided keyList.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyList"></param>
        /// <returns> If search went well, returns a list of data that contains provided
        /// keys, otherwise - an empty list.</returns>
        public static List<ComparedParam> FindDataByKeys(List<ComparedParam> data, List<string> keyList)
        {
            List<ComparedParam> foundPairs = new();

            foreach (var item in data)
            {
                for (int i = 0; i < keyList.Count; ++i)
                {
                    if (item.SourcePair.Key == keyList[i] || item.TargetPair.Key == keyList[i])
                    {
                        foundPairs.Add(item);
                        break;
                    }
                }
            }

            return foundPairs;
        }
    }
}
