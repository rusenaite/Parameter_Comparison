using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ParameterComparison.ConfigurationComparison;

namespace ParameterComparison
{
    public class ConfigurationDataPrinter
    {

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

        /// <summary>
        /// Method prints column names for configuration data.
        /// </summary>
        public static void PrintColumnNames()
        {
            Console.Write("\n\n\n{0}\t{1}\t\t\t{2}\t\t\t{3}", "Status", "ID", "Source Value", "Target Value");
        }

        /// <summary>
        /// Method prints string type provided KeyValuePair type ID pair.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="pair"></param>
        public static void PrintAsStringTypeIdPair(string action, KeyValuePair<string, string> pair)
        {
            Console.Write("\n{0}\t{1}\t\t\t\t\t{2}", action, pair.Key, pair.Value);
        }

        /// <summary>
        /// Method seperately prints data with string type keys (IDs) (as it is not included
        /// to the parameter comparison).
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public static void PrintStringTypeIdData(Dictionary<string, string> stringSourceData, Dictionary<string, string> stringTargetData)
        {
            foreach (var srcPair in stringSourceData)
            {
                PrintAsStringTypeIdPair("-", srcPair);
            }

            foreach (var trgPair in stringTargetData)
            {
                PrintAsStringTypeIdPair("-", trgPair);
            }
        }

        /// <summary>
        /// Method prints summary of provided calculated comparison results.
        /// </summary>
        /// <param name="count"></param>
        public static void PrintComparisonResultsSummary((string result, int count)[] count)
        {
            count.Where(item => item.result != null && item.result.Any() && count != null).ToList().ForEach(tuple =>
            {
                Console.Write($"{tuple.result}:{tuple.count} ");
            });
        }

        /// <summary>
        /// Method prints compared data pairs from a list.
        /// </summary>
        /// <param name="comparedData"></param>
        public static void PrintComparedData(List<ComparedParam> comparedData)
        {
            foreach(ComparedParam pair in comparedData)
            {
                PrintComparedPair(pair);
            }
        }

        /// <summary>
        /// Method prints compared parameter pair.
        /// </summary>
        /// <param name="pair"></param>
        public static void PrintComparedPair(ComparedParam pair)
        {
            Console.BackgroundColor = pair.Color;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t\t\t{2}\t\t\t\t\t{3}", pair.Action, pair.SourcePair.Key, pair.SourcePair.Value, pair.TargetPair.Value);
            Console.ResetColor();
        }
    }
}
