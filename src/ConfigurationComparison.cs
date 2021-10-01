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
        public static void CheckForEqualData(Dictionary<int, string> sourceData, Dictionary<int, string> targetData)
        {
            if(sourceData == targetData)
            {
                foreach(var srcPair in sourceData)
                {
                    foreach (var trgPair in targetData)
                    {
                        PrintAsUnchanged("U", srcPair, trgPair);
                    }
                }
            } 
        }

        /// <summary>
        /// Method finds and converts string type keys that are integers to int type variables.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If convertion went well returns dictionary collection of only int key type, 
        /// otherwise - an empty dictionary. </returns>
        public static Dictionary<int, string> GetIntTypeKeys(Dictionary<string, string> data)
        {
            Dictionary<int, string> intKeyData = new();

            foreach (var pair in data)
            {
                bool success = int.TryParse(pair.Key, out int key);
                if (success)
                {
                    intKeyData.Add(key, pair.Value);
                }
            }

            return intKeyData;
        }

        /// <summary>
        /// Method finds string type keys from mixed-key-type dictionary.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If search went well returns dictionary collection of only string key type,
        /// otherwise - an empty dictionary. </returns>
        public static Dictionary<string, string> GetStringTypeKeys(Dictionary<string, string> data)
        {
            Dictionary<string, string> stringKeyData = new();

            foreach (var pair in from pair in data
                                 let success = int.TryParse(pair.Key, out int key)
                                 where !success
                                 select pair)
            {
                stringKeyData.Add(pair.Key, pair.Value);
            }

            return stringKeyData;
        }

        /// <summary>
        /// Method finds maximum key (ID) value from 2 given dictionaries.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns> If search went well, returns an integer of maximum ID value, 
        /// otherwise - 0. </returns>
        public static int FindMaximumKeyValue(Dictionary<int, string> source, Dictionary<int, string> target)
        {
            int sourceMaxKey = source.Aggregate((l, r) => l.Key > r.Key ? l : r).Key;
            int targetMaxKey = target.Aggregate((l, r) => l.Key > r.Key ? l : r).Key;

            return sourceMaxKey.CompareTo(targetMaxKey);
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

        /// <summary>
        /// Method prints parameter list - paramater ID, value and comparison result of
        /// 2 configuration files.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public void ViewParameterList(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<string, string> stringSourceData = GetStringTypeKeys(sourceData);
            Dictionary<string, string> stringTargetData = GetStringTypeKeys(targetData);

            Dictionary<int, string> intSourceData = GetIntTypeKeys(sourceData);
            Dictionary<int, string> intTargetData = GetIntTypeKeys(targetData);

            PrintStringTypeIdData(stringSourceData, stringTargetData);
            PrintIntTypeIdData(intSourceData, intTargetData);
        }

        /// <summary>
        /// Method prints comparison result summary.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public void ViewComparisonResultsSummary(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<int, string> intSourceData = GetIntTypeKeys(sourceData);
            Dictionary<int, string> intTargetData = GetIntTypeKeys(targetData);

            ResultDictionaries resultDictionaries = CompareConfigurations(intSourceData, intTargetData);

            ResultCount count = new();

            count.unchanged = resultDictionaries.unchanged.Count;
            count.modified = resultDictionaries.modified.Count;
            count.removed = resultDictionaries.removed.Count;
            count.added = resultDictionaries.added.Count;

            PrintComparisonResultsSummary(count);
        }

        public void FilterParametersById(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string value)
        {
            Dictionary<int, string> intSourceData = GetIntTypeKeys(sourceData);
            Dictionary<int, string> intTargetData = GetIntTypeKeys(targetData);

            ResultDictionaries resultDictionaries = CompareConfigurations(intSourceData, intTargetData);

            Dictionary<int, string> comparedData = CombineDictionaries(resultDictionaries);

            List<string> foundKeys = SearchForValue(comparedData, value);
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

        /// <summary>
        /// Method searches of a specific key value in a list of keys.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        /// <returns> If search went well, returns a list of string type keys, 
        /// otherwise - an empty list. </returns>
        public List<string> SearchForValue(Dictionary<int, string> data, string value)
        {
            List<string> keys = GetKeysAsStrings(data);
            string enteredVal = value.ToString();

            List<string> foundKeys = new();

            bool found;

            foreach(var key in keys)
            {
                found = key.StartsWith(enteredVal, false, CultureInfo.InvariantCulture);

                if (found)
                {
                    foundKeys.Add(key);
                }
                else
                {
                    continue;
                }
            }

            return foundKeys;
        }

        /// <summary>
        /// Method gets integer type key values from given dictionary and converts it to 
        /// a list of strings.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If convertion went well, returns a list of string type keys, 
        /// otherwise - an empty list. </returns>
        public List<string> GetKeysAsStrings(Dictionary<int, string> data)
        {
            Dictionary<int, string>.KeyCollection dataKeys = data.Keys;

            List<int> intListOfKeys = dataKeys.ToList();
            List<string> stringListOfKeys = new();

            intListOfKeys.ForEach(i => stringListOfKeys.Add(i.ToString()));

            return stringListOfKeys;
        }

        public void PrintConfigData(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<string, string> srcStringKeyData = GetStringTypeKeys(sourceData);
            Dictionary<string, string> trgStringKeyData = GetStringTypeKeys(targetData);


        }

    }
}
