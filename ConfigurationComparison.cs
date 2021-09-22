using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ConfigurationComparison : IConfigChangesPrinter
    {
        public static void CompareConfigurations(Dictionary<string, string> sourceFile, Dictionary<string, string> targetFile)
        {


        }

        public void PrintDeviceConfigInfo(Dictionary<string, string> sourceFile, Dictionary<string, string> targetFile)
        {
            ConfigData pair = new ConfigData();
            DeviceInfo sourceDevice = new DeviceInfo();
            
            if (sourceFile.TryGetValue("ConfigurationVersion", out string configversion))
            {
                Console.WriteLine("{0}: {1}", "Configuration Version", configversion);
            }

            if (sourceFile.TryGetValue("HwVersion", out string hwversion))
            {
                Console.WriteLine("{0}: {1}", "Hw Version", hwversion);
            }
            
        }
    }
}
