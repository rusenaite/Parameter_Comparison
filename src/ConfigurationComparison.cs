using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParameterComparison
{
    public class ConfigurationComparison : ConfigurationDataPrinter, IConfigFilePrinter
    {
        public struct ResultCount
        {
            public int unchanged;
            public int modified;
            public int removed;
            public int added;
        }

        public struct ResultDictionaries
        {
            public Dictionary<KeyValuePair<int, string>, KeyValuePair<int, string>> unchanged;
            public Dictionary<KeyValuePair<int, string>, KeyValuePair<int, string>> modified;
            public Dictionary<int, string> removed;
            public Dictionary<int, string> added;
        }

        /// <summary>
        /// Method checks if the data is equal, and if it is, prints all configuration
        /// data marked as "Unchanged".
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public static bool CheckForEqualData(Dictionary<int, string> sourceData, Dictionary<int, string> targetData)
        {
            if (sourceData != targetData)
            {
                return false;
            }
            else
            {
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
                            PrintAsUnchanged("U", srcPair, trgPair);
                        }
                    }
                }
                return true;
            }
        }

        public static ResultDictionaries CompareConfigurations(Dictionary<int, string> intSrcData, Dictionary<int, string> intTrgData)
        {
            Dictionary<KeyValuePair<int, string>, KeyValuePair<int, string>> unchanged = new();
            Dictionary<KeyValuePair<int, string>, KeyValuePair<int, string>> modified = new();
            Dictionary<int, string> removed = new();
            Dictionary<int, string> added = new();

            foreach (var srcPair in intSrcData)
            {
                foreach (var trgPair in intTrgData)
                {
                    if (srcPair.Key > trgPair.Key)
                    {
                        continue;
                    }

                    if (srcPair.Key == trgPair.Key)
                    {
                        if (srcPair.Value == trgPair.Value)
                        {
                            unchanged.Add(srcPair, trgPair);
                            break;
                        }
                        else if (srcPair.Value != trgPair.Value
                            && intSrcData.ContainsKey(trgPair.Key)
                            && intTrgData.ContainsKey(srcPair.Key))
                        {
                            modified.Add(srcPair, trgPair);
                            break;
                        }
                    }

                    else if (intSrcData.ContainsKey(trgPair.Key) && !intTrgData.ContainsKey(srcPair.Key))
                    {
                        removed.Add(srcPair.Key, srcPair.Value);
                        break;
                    }

                    else if (!intSrcData.ContainsKey(trgPair.Key) && intTrgData.ContainsKey(srcPair.Key))
                    {
                        added.Add(trgPair.Key, trgPair.Value);
                    }
                }
            }

            ResultDictionaries results = new();
            results.unchanged = unchanged;
            results.modified = modified;
            results.removed = removed;
            results.added = added;

            return results;
        }

        public static List<ComparedParam> CompareConfig(Dictionary<int, string> intSrcData, Dictionary<int, string> intTrgData)
        {
            List<ComparedParam> resultsList = new List<ComparedParam>();

            foreach (var srcPair in intSrcData)
            {
                foreach (var trgPair in intTrgData)
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
                            && intSrcData.ContainsKey(trgPair.Key)
                            && intTrgData.ContainsKey(srcPair.Key))
                        {
                            ComparedParam pair = SetComparedPair(srcPair, trgPair, "M", ConsoleColor.Yellow);
                            resultsList.Add(pair);

                            break;
                        }
                    }

                    else if (intSrcData.ContainsKey(trgPair.Key) && !intTrgData.ContainsKey(srcPair.Key))
                    {
                        ComparedParam pair = SetComparedPair(srcPair, trgPair, "R", ConsoleColor.Red);
                        resultsList.Add(pair);

                        break;
                    }

                    else if (!intSrcData.ContainsKey(trgPair.Key) && intTrgData.ContainsKey(srcPair.Key))
                    {
                        ComparedParam pair = SetComparedPair(srcPair, trgPair, "A", ConsoleColor.Green);
                        resultsList.Add(pair);
                    }
                }
            }

            return resultsList;
        }

        
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
            pair.Color = color;

            return pair;
        }
        

        public static bool ContainsKeys(Dictionary<string, string> data, string[] keys)
        {
            return keys.Any() && keys.All(data.ContainsKey);
        }

        public void ViewDeviceConfigInfo(Dictionary<string, string> data, string path)
        {
            data.GetStringTypeKeys();
            DeviceInfo devInfo = new DeviceInfo();

            string[] keys = { devInfo.ConfigurationVersion, devInfo.HwVersion, 
                            devInfo.Title, devInfo.MinConfigurationVersion, devInfo.FmType };

            if (ContainsKeys(data.GetStringTypeKeys(), keys))
            {
                PrintDeviceConfigInfo(data.GetStringTypeKeys(), path);
            }
        }

        /// <summary>
        /// Method prints parameter list - paramater ID, value and comparison result of
        /// 2 configuration files.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public void ViewParameterList(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            PrintColumnNames();
            PrintStringTypeIdData(sourceData.GetStringTypeKeys().RemovedDeviceInfo(), targetData.GetStringTypeKeys().RemovedDeviceInfo());
            PrintIntTypeIdData(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());
        }

        /// <summary>
        /// Method prints comparison result summary.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public void ViewComparisonResultsSummary(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            ResultDictionaries resultDictionaries = CompareConfigurations(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            ResultCount count = new();

            count.unchanged = resultDictionaries.unchanged.Count;
            count.modified = resultDictionaries.modified.Count;
            count.removed = resultDictionaries.removed.Count;
            count.added = resultDictionaries.added.Count;

            PrintComparisonResultsSummary(count);
        }

        /// <summary>
        /// Method prints filtered parameters by a given key value (filter).
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <param name="filter"></param>
        public void ViewFilteredParameters(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string filter)
        {
            List<ComparedParam> list = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());
            List<ComparedParam> foundParamList = SearchForValue(list, filter);

            PrintComparedData(foundParamList);
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
            List<int> sourceKeys = new();
            List<int> targetKeys = new();

            // get all keys
            foreach(var item in data)
            {
                sourceKeys.Add(item.SourcePair.Key);
                targetKeys.Add(item.TargetPair.Key);
            }

            // combine src and trg keys in one list (no duplicates)
            List<int> allIntKeys = sourceKeys.Union(targetKeys).ToList();
            List<string> keysAsStrings = GetKeysAsStrings(allIntKeys);
            List<string> foundKeyList = new();

            // search for value
            bool found;

            foreach (var key in keysAsStrings)
            {
                found = key.StartsWith(value, false, CultureInfo.InvariantCulture);

                if (found)
                {
                    foundKeyList.Add(key);
                }
            }

            // convert to int key list
            List<int> intList = foundKeyList.Select(key => Convert.ToInt32(key)).ToList();
            List<ComparedParam> foundPairs = new();

            // find keys in data that were found by filter value
            foreach (var item in data)
            {
                for (int i = 0; i < intList.Count; ++i)
                {
                    if (item.SourcePair.Key == intList[i] || item.TargetPair.Key == intList[i])
                    {
                        foundPairs.Add(item);
                        break;
                    }
                }
            }

            return foundPairs;
        }

        /// <summary>
        /// Method gets an integer type list of keys and converts it to 
        /// a list of strings.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If convertion went well, returns a list of string type keys, 
        /// otherwise - an empty list. </returns>
        public static List<string> GetKeysAsStrings(List<int> keys)
        {
            List<string> stringListOfKeys = new();

            keys = keys.ToList();
            keys.ForEach(i => stringListOfKeys.Add(i.ToString()));

            return stringListOfKeys;
        }

        public void ViewParamByComparisonResult(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string choice)
        {
            ResultDictionaries result = CompareConfigurations(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            switch (choice)
            {
                case "U":

                    break;
                case "M":

                    break;
                case "R":

                    break;
                case "A":

                    break;

            }
        }
    }
}
