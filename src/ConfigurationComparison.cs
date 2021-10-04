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

        public void FilterParametersById(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string value)
        {
            ResultDictionaries resultDictionaries = CompareConfigurations(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            Dictionary<int, string> combinedComparedData = CombineDictionaries(resultDictionaries);

            // Found keys
            combinedComparedData.SearchForValue(value);

        }

        public Dictionary<int, string> CombineDictionaries(ResultDictionaries dictionaries)
        {
            var d1 = dictionaries.unchanged.Keys;
            var d2 = dictionaries.modified.Keys;
            var d3 = dictionaries.removed;
            var d4 = dictionaries.added;

            Dictionary<int, string> comparedData = d1.Concat(d2).Concat(d3).Concat(d4).GroupBy(d => d.Key)
                                                .ToDictionary(d => d.Key, d => d.First().Value);

            return comparedData;
        }

        public void ViewFilteredParameters(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            
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
