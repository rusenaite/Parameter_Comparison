using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void CompareConfigurations(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            Dictionary<int, string> newSrcData = RemoveStringTypeId(sourceData);
            Dictionary<int, string> newTrgData = RemoveStringTypeId(targetData);

            int maximumSourceId = newSrcData.Aggregate((l, r) => l.Key > r.Key ? l : r).Key;
            int maximumTargetId = newTrgData.Aggregate((l, r) => l.Key > r.Key ? l : r).Key;

            int maxId = maximumSourceId.CompareTo(maximumTargetId);

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
    }
}
