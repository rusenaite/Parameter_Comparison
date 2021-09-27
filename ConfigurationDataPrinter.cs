using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ConfigurationDataPrinter : IConfigFilePrinter
    {
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

        public void PrintConfigData(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            //
        }
    }
}
