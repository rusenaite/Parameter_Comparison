using System.Collections.Generic;
using System.Linq;

namespace ParameterComparison.src.ConfigDataProcessing
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
            return data.Where(pair => !int.TryParse(pair.Key, out int key)).ToDictionary(p => p.Key, p => p.Value);
        }

        public static List<ComparedParam> GetStringTypeKeys(this List<ComparedParam> data)
        {
            foreach (var item in data.ToList())
            {
                if (int.TryParse(item.SourcePair.Key, out _) | int.TryParse(item.TargetPair.Key, out _))
                {
                    data.Remove(item);
                }
            }

            return data;
        }

        public static List<ComparedParam> GetIntTypeKeys(this List<ComparedParam> data)
        {
            foreach (var item in data.ToList())
            {
                if (!int.TryParse(item.SourcePair.Key, out _) | !int.TryParse(item.TargetPair.Key, out _))
                {
                    data.Remove(item);
                }
            }

            return data;
        }

        /// <summary>
        /// Method removes string type keys (and its value) from the list which contain 
        /// device information.
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns> If removing went well returns list collection of string key type
        /// without device information, otherwise - an empty list. </returns>
        public static List<ComparedParam> RemovedDeviceInfo(this List<ComparedParam> list)
        {
            string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                              DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

            for (int i = 0; i < keys.Length; ++i)
            {
                foreach (var item in list)
                {
                    if (item.SourcePair.Key == keys[i] | item.TargetPair.Key == keys[i])
                    {
                        list.Remove(item);
                    }
                }
            }

            return list;
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
