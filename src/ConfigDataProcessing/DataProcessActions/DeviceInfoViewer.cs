using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.ConfigDataProcessing.DataProcessActions
{
    public class DeviceInfoViewer : ConfigurationComparison
    {
        /// <summary>
        /// Method prints device configuration information.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public static void ViewDeviceConfigInfo(Dictionary<string, string> data, string path)
        {
            data.GetStringTypeKeys();

            DeviceInfo devInfo = new DeviceInfo();
            string[] keys = { devInfo.ConfigurationVersion, devInfo.HwVersion, devInfo.Title,
                              devInfo.MinConfigurationVersion, devInfo.FmType, devInfo.SpecId };

            if (ContainsKeys(data.GetStringTypeKeys(), keys))
            {
                PrintDeviceConfigInfo(data.GetStringTypeKeys(), path);
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

            DeviceInfo devInfo = new();
            string[] keys = { devInfo.ConfigurationVersion, devInfo.HwVersion, devInfo.Title,
                            devInfo.MinConfigurationVersion, devInfo.FmType, devInfo.SpecId };

            int i = 0;
            foreach (var pair in data)
            {
                if (i < keys.Length && pair.Key == keys[i])
                {
                    Console.WriteLine("{0}: {1}", keys[i], pair.Value);
                    ++i;
                }
            }

            Console.WriteLine("\n");
        }
    }
}
