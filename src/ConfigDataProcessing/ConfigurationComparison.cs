using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParameterComparison
{
    public class ConfigurationComparison
    {

        public (string result, int count)[] resultCount = new [] { ("U", 0), ("M", 0), ("R", 0), ("A", 0) };

        /// <summary>
        /// Method compares integer-key-type source and target data and based on comparison
        /// result, puts results into a list of ComparedParam objects.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <returns> If comparison went well, returns a list of compared parameters, 
        /// otherwise - returns an empty list. </returns>
        public static List<ComparedParam> CompareConfig(Dictionary<int, string> sourceData, Dictionary<int, string> targetData)
        {
            List<ComparedParam> resultsList = new List<ComparedParam>();

            foreach (var srcPair in sourceData)
            {
                foreach (var trgPair in targetData)
                {
                    if (srcPair.Key > trgPair.Key)
                    {
                        continue;
                    }

                    if (srcPair.Key == trgPair.Key)
                    {
                        if (srcPair.Value == trgPair.Value)
                        {
                            ComparedParam pair = SetComparedPair(srcPair, trgPair, "U", ConsoleColor.Gray);
                            resultsList.Add(pair);

                            break;
                        }
                        else if (srcPair.Value != trgPair.Value
                            && sourceData.ContainsKey(trgPair.Key)
                            && targetData.ContainsKey(srcPair.Key))
                        {
                            ComparedParam pair = SetComparedPair(srcPair, trgPair, "M", ConsoleColor.Yellow);
                            resultsList.Add(pair);

                            break;
                        }
                    }

                    else if (sourceData.ContainsKey(trgPair.Key) && !targetData.ContainsKey(srcPair.Key))
                    {
                        ComparedParam pair = SetComparedPair(srcPair, trgPair, "R", ConsoleColor.Red);
                        resultsList.Add(pair);

                        break;
                    }

                    else if (!sourceData.ContainsKey(trgPair.Key) && targetData.ContainsKey(srcPair.Key))
                    {
                        ComparedParam pair = SetComparedPair(srcPair, trgPair, "A", ConsoleColor.Green);
                        resultsList.Add(pair);
                    }
                }
            }

            return resultsList;
        }

        /// <summary>
        /// Method creates a ComparedParam object of compared pair data.
        /// </summary>
        /// <param name="sourcePair"></param>
        /// <param name="targetPair"></param>
        /// <param name="action"></param>
        /// <param name="color"></param>
        /// <returns> If object generation went well, returns a ComparedParam object,
        /// otherwise - an empty ComparedParam object.</returns>
        public static ComparedParam SetComparedPair(KeyValuePair<int, string> sourcePair,
                                                    KeyValuePair<int, string> targetPair, string action,
                                                    ConsoleColor color)
        {
            ComparedParam pair = new ComparedParam();

            if (sourcePair.Value != null)
            {
                pair.SourcePair = sourcePair;
            }

            if (targetPair.Value != null)
            {
                pair.TargetPair = targetPair;
            }

            pair.Action = action;

            return pair;
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
            List<string> keysAsStrings = data.GetKeys().GetKeysAsStrings();
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

            List<int> intList = foundKeyList.GetKeysAsIntegers();
            List<ComparedParam> foundPairs = FindDataByKeys(data, intList);

            return foundPairs;
        }

        /// <summary>
        /// Method searches for parameter data that contains keys from provided keyList.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyList"></param>
        /// <returns> If search went well, returns a list of data that contains provided
        /// keys, otherwise - an empty list.</returns>
        public static List<ComparedParam> FindDataByKeys(List<ComparedParam> data, List<int> keyList)
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
