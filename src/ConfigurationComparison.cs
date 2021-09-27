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
        /// Method looks for key-value pairs where keys are only string type.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> A dictionary collection where keys are only string type,
        /// otherwise - returns an empty dictionary. </returns>
        public static Dictionary<string, string> GetStringTypeId(Dictionary<string, string> data)
        {
            Dictionary<string, string> stringTypeIdData = new();
            string str = "";

            foreach (var pair in data.Where(item => item.Key.GetType() == str.GetType()))
            {
                stringTypeIdData.Add(pair.Key, pair.Value);
            }

            return stringTypeIdData;
        }

        /// <summary>
        /// Method removes key-value pairs from the passed dictionary collection if it's
        /// key (ID) type is string.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If removing went well, returns dictionary collection without string
        /// type keys, otherwise returns an empty dictionary. </returns>
        public static Dictionary<int, string> RemoveStringTypeId(Dictionary<string, string> data)
        {
            Dictionary<string, string> stringTypeIdData = GetStringTypeId(data);

            foreach (string key in stringTypeIdData.Keys)
            {
                data.Remove(key);
            }

            Dictionary<int, string> newData = new Dictionary<int, string>((IDictionary<int, string>)data);

            return newData;
        }

        public bool CheckExtremeCases(Dictionary<int, string> sourceData, Dictionary<int, string> targetData)
        {
            // Implementation

            return true;
        }

        public static bool CheckIfDataIsEqual(Dictionary<int, string> sourceData, Dictionary<int, string> targetData)
        {
            return sourceData == targetData;
        }

        public static bool CheckIfDataExists(Dictionary<int, string> data)
        {
            return data == null;
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
        /// <returns> If convertion went well returns dictionary collection of only int key type 
        /// dictionary collection, otherwise - an empty dictionary. </returns>
        public static Dictionary<int, string> GetIntTypeKeys(Dictionary<string, string> data)
        {
            Dictionary<int, string> newData = new();

            foreach (var pair in data)
            {
                bool success = int.TryParse(pair.Key, out int keyAsInt);
                if (success)
                {
                    newData.Add(keyAsInt, pair.Value);
                }
            }

            return newData;
        }


        public static void CompareConfigurations(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<int, string> newSrcData = RemoveStringTypeId(sourceData);
            Dictionary<int, string> newTrgData = RemoveStringTypeId(targetData);

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

        public void CompareData(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<int, string> intSourceData = GetIntTypeKeys(sourceData);
            Dictionary<int, string> intTargetData = GetIntTypeKeys(targetData);

            int maxCount = intSourceData.Count().CompareTo(intTargetData.Count());

            foreach (var srcPair in intSourceData)
            {
                foreach (var trgPair in intTargetData)
                {
                    if (srcPair.Key == trgPair.Key)
                    {
                        if(srcPair.Value == trgPair.Value)
                        {
                            PrintAsUnchanged("U", srcPair, trgPair);
                        }
                        
                        if(srcPair.Value != trgPair.Value)
                        {
                            PrintAsModified("M", srcPair, trgPair);
                        }
                        
                    }
                    if (intSourceData.ContainsKey(trgPair.Key) && !intTargetData.ContainsKey(srcPair.Key))
                    {
                        PrintAsRemoved("R", srcPair);
                    }

                    if (!intSourceData.ContainsKey(trgPair.Key) && intTargetData.ContainsKey(srcPair.Key))
                    {
                        PrintAsAdded("A", srcPair);
                    }
                }

            }
        }

    }
}
