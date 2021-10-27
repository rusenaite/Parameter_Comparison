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
        /// Method finds string type keys from mixed key type dictionary.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If search went well returns dictionary collection of only string key type,
        /// otherwise - an empty dictionary. </returns>
        public static Dictionary<string, string> GetStringTypeKeys(this Dictionary<string, string> data)
        {
            return data.Where(pair => !(int.TryParse(pair.Key, out int key))).ToDictionary(p => p.Key, p => p.Value);
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
            string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                              DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

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
        /// <returns> If operation went well, returns a list of string type keys, otherwise - 
        /// returns an empty list. </returns>
        public static List<string> GetKeys(this List<ComparedParam> data)
        {
            List<string> sourceKeys = new();
            List<string> targetKeys = new();

            foreach (var item in data)
            {
                sourceKeys.Add(item.SourcePair.Key);
                targetKeys.Add(item.TargetPair.Key);
            }

            List<string> allKeys = sourceKeys.Union(targetKeys).ToList();

            return allKeys;
        }

    }
}
