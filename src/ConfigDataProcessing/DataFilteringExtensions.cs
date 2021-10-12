using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public static class DataFilteringExtensions
    {
        /// <summary>
        /// Method finds and converts string type keys that are integers to int type variables.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If convertion went well returns dictionary collection of only int key type, 
        /// otherwise - an empty dictionary. </returns>
        public static Dictionary<int, string> GetIntTypeKeys(this Dictionary<string, string> data)
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
        /// Method finds string type keys from mixed key type dictionary.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If search went well returns dictionary collection of only string key type,
        /// otherwise - an empty dictionary. </returns>
        public static Dictionary<string, string> GetStringTypeKeys(this Dictionary<string, string> data)
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
        /// Method removes string type keys (and its value) from the dictionary which contain 
        /// device information.
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns> If removing went well returns dictionary collection of string key type
        /// without device information, otherwise - an empty dictionary. </returns>
        public static Dictionary<string, string> RemovedDeviceInfo(this Dictionary<string, string> stringData)
        {
            DeviceInfo devInfo = new();
            string[] keys = { devInfo.ConfigurationVersion, devInfo.HwVersion, devInfo.Title,
                            devInfo.MinConfigurationVersion, devInfo.FmType, devInfo.SpecId };

            for (int i = 0; i < keys.Length; ++i)
            {
                if (stringData.ContainsKey(keys[i]))
                {
                    stringData.Remove(keys[i]);
                }
            }

            return stringData;
        }

        /// <summary>
        /// Method gets all source and target keys from data list without duplicates
        /// and puts it into a list.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If operation went well, returns a list of integer type keys, otherwise - 
        /// returns an empty list. </returns>
        public static List<int> GetKeys(this List<ComparedParam> data)
        {
            List<int> sourceKeys = new();
            List<int> targetKeys = new();

            foreach (var item in data)
            {
                sourceKeys.Add(item.SourcePair.Key);
                targetKeys.Add(item.TargetPair.Key);
            }

            List<int> allIntKeys = sourceKeys.Union(targetKeys).ToList();

            return allIntKeys;
        }

        /// <summary>
        /// Method gets an integer type list of keys and converts it to a list of strings.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns> If convertion went well, returns a list of string type keys, 
        /// otherwise - an empty list. </returns>
        public static List<string> GetKeysAsStrings(this List<int> keys)
        {
            List<string> stringList = new();

            if (keys != null)
                stringList = keys.Select(key => Convert.ToString(key)).ToList();
            
            return stringList;
        }

        /// <summary>
        /// Method gets an string type list of keys and converts it to a list of integers.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns> If convertion went well, returns a list of int type keys, 
        /// otherwise - an empty list. </returns>
        public static List<int> GetKeysAsIntegers(this List<string> keys)
        {
            List<int> intList = new();

            if (keys != null)
                intList = keys.Select(key => Convert.ToInt32(key)).ToList();

            return intList;
        }
    }
}
