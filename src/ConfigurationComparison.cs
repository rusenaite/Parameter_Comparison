using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParameterComparison
{
    public class ConfigurationComparison : IConfigChangesPrinter
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
        /// Method converts string type keys that are integers to int type variables.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If convertion went well returns dictionary collection of only int key type 
        /// dictionary collection, otherwise - an empty dictionary. </returns>
        public static Dictionary<int, string> ConvertStringTypeKeysToInt(Dictionary<string, string> data)
        {
            Dictionary<int, string> newData = new();

            foreach (var pair in data)
            {
                bool success = int.TryParse(pair.Key, out int keyAsInt);
                if(success)
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

        /// <summary>
        /// Method prints configuration information of the given device.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public void PrintDeviceConfigInfo(Dictionary<string, string> data, string path)
        {
            string fileName = Path.GetFileName(path).ToUpper();
            Console.WriteLine(fileName);

            if (data.TryGetValue("ConfigurationVersion", out string configversion))
                Console.WriteLine("{0}: {1}", "Configuration Version", configversion);

            if (data.TryGetValue("HwVersion", out string hwversion))
                Console.WriteLine("{0}: {1}", "Hw Version", hwversion);

            if (data.TryGetValue("Title", out string title))
                Console.WriteLine("{0}: {1}", "Title", title);

            if (data.TryGetValue("MinConfiguratorVersion", out string minConfiguration))
                Console.WriteLine("{0}: {1}", "Minimum Configuration", minConfiguration);

            if (data.TryGetValue("FmType", out string fmType))
                Console.WriteLine("{0}: {1}", "Fm Type", fmType);

            if (data.TryGetValue("SpecId", out string specId))
                Console.WriteLine("{0}: {1}\n\n", "Spec Id", specId);
        }

        public void PrintConfigData(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<int, string> intSourceData = ConvertStringTypeKeysToInt(sourceData);
            Dictionary<int, string> intTargetData = ConvertStringTypeKeysToInt(targetData);

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

                    else
                    {
                        //Console.WriteLine("-\t{0}\t{1}\t{2}", srcPair.Key, srcPair.Value, "-");
                        //Console.WriteLine("-\t{0}\t{1}\t{2}", trgPair.Key, "-", trgPair.Value);
                    }
                    
                }

            }
        }

        public static void PrintAsUnchanged(string action, KeyValuePair<int, string> sourcePair, KeyValuePair<int, string> targetPair)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t{2}\t{3}", action, sourcePair.Key, sourcePair.Value, targetPair.Value);
            Console.ResetColor();
        }

        public static void PrintAsAdded(string action, KeyValuePair<int, string> targetPair)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t{2}\t{3}", action, targetPair.Key, "-", targetPair.Value);
            Console.ResetColor();
        }

        public static void PrintAsRemoved(string action, KeyValuePair<int, string> sourcePair)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t{2}\t{3}", action, sourcePair.Key, sourcePair.Value, "-");
            Console.ResetColor();
        }

        public static void PrintAsModified(string action, KeyValuePair<int, string> sourcePair, KeyValuePair<int, string> targetPair)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t{2}\t{3}", action, sourcePair.Key, sourcePair.Value, targetPair.Value);
            Console.ResetColor();
        }

    }
}
