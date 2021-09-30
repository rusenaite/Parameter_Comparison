using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParameterComparison
{
    public class ConfigurationComparison : ConfigurationDataPrinter, IConfigFilePrinter
    {
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

        public static void CompareConfigurations(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<int, string> newSrcData = GetIntTypeKeys(sourceData);
            Dictionary<int, string> newTrgData = GetIntTypeKeys(targetData);

            int maxId = FindMaximumKeyValue(newSrcData, newTrgData);

            for (var id = 0; id <= maxId; ++id)
            {
                if (newSrcData.ContainsKey(id) && newTrgData.ContainsKey(id))
                {
                    
                }
                else if (newSrcData.ContainsKey(id) && !newTrgData.ContainsKey(id))
                {

                }
                else if (!newSrcData.ContainsKey(id) && newTrgData.ContainsKey(id))
                {

                }
                else
                {
                    continue;
                }
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
            Dictionary<string, string> stringSourceData = GetStringTypeKeys(sourceData);
            Dictionary<string, string> stringTargetData = GetStringTypeKeys(targetData);

            Dictionary<int, string> intSourceData = GetIntTypeKeys(sourceData);
            Dictionary<int, string> intTargetData = GetIntTypeKeys(targetData);

            PrintStringTypeIdData(stringSourceData, stringTargetData);
            PrintIntTypeIdData(intSourceData, intTargetData);
        }

        public void PrintConfigData(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<string, string> srcStringKeyData = GetStringTypeKeys(sourceData);
            Dictionary<string, string> trgStringKeyData = GetStringTypeKeys(targetData);


        }

    }
}
