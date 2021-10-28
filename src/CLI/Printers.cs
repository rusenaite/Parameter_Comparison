﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ParameterComparison.ConfigurationComparison;

namespace ParameterComparison
{
    public class Printers
    {

        /// <summary>
        /// Method prints compared data pairs from a list.
        /// </summary>
        /// <param name="comparedData"></param>
        public static void PrintComparedData(List<ComparedParam> comparedData)
        {
            Console.WriteLine(new Columns().ToString());

            foreach (ComparedParam pair in comparedData)
            {
                SetBackgroundColor(pair);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(pair);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Method assigns a custom console background color for a compared parameter depending on
        /// it's comparison result.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns> If assignment went well, returns ConsoleColor value, otherwise - 
        /// a default background color. </returns>
        public static ConsoleColor SetBackgroundColor(ComparedParam pair)
        {
            return pair.Action switch
            {
                ParamAction.U => Console.BackgroundColor = ConsoleColor.Gray,
                ParamAction.M => Console.BackgroundColor = ConsoleColor.Yellow,
                ParamAction.R => Console.BackgroundColor = ConsoleColor.Red,
                ParamAction.A => Console.BackgroundColor = ConsoleColor.Green,
                _ => Console.BackgroundColor,
            };
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

        /// <summary>
        /// Method seperately prints data with string type keys (IDs) (as it is not included
        /// to the parameter comparison).
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public static void PrintStringTypeIdData(Dictionary<string, string> stringSourceData, Dictionary<string, string> stringTargetData)
        {
            stringSourceData.ToList().ForEach(pair =>
            {
                Console.Write("\n".PadRight(Columns.ColumnWidth) + pair.Key.PadRight(Columns.ColumnWidth) + pair.Value.PadRight(Columns.ColumnWidth));
            });

            stringTargetData.ToList().ForEach(pair =>
            {
                Console.Write("\n".PadRight(Columns.ColumnWidth) + pair.Key.PadRight(Columns.ColumnWidth) + pair.Value.PadRight(Columns.ColumnWidth));
            });
        }

        /// <summary>
        /// Method prints summary of provided calculated comparison results.
        /// </summary>
        /// <param name="count"></param>
        public static void PrintComparisonResultsSummary((ParamAction result, int count)[] count)
        {
            count.Where(item => count != null).ToList().ForEach(tuple =>
            {
                Console.Write($"{tuple.result}:{tuple.count} ");
            });
        }
    }
}