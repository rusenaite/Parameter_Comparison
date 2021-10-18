using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class DeviceInfoViewer : ConfigurationComparison, IDeviceInfoAction
    {

        /// <summary>
        /// Method prints configuration information of source and target devices.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <param name="path"></param>
        public void View(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string sourcePath, string targetPath)
        {
            string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                              DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

            if (ContainsKeys(sourceData.GetStringTypeKeys(), keys) && ContainsKeys(targetData.GetStringTypeKeys(), keys))
            {
                PrintDeviceConfigInfo(sourceData.GetStringTypeKeys(), sourcePath);
                PrintDeviceConfigInfo(targetData.GetStringTypeKeys(), targetPath);
            }
        }

        /// <summary>
        /// Method prints configuration information of the given device.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public static void PrintDeviceConfigInfo(Dictionary<string, string> data, string path)
        {
            string fileName = Path.GetFileName(path).ToUpper();
            Console.WriteLine(fileName);

            string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                              DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

            int i = 0;
            foreach (var pair in data)
            {
                if (i < keys.Length && pair.Key == keys[i])
                {
                    Console.WriteLine($"{keys[i]}: {pair.Value}");
                    ++i;
                }
            }

            Console.WriteLine("\n");
        }
    }
}
