using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ConfigurationDataPrinter
    {
        public struct ResultCount
        {
            public int unchanged;
            public int modified;
            public int removed;
            public int added;
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

        public static void PrintAsUnchanged(string action, KeyValuePair<int, string> sourcePair, KeyValuePair<int, string> targetPair)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t\t\t{2}\t\t\t{3}", action, sourcePair.Key, sourcePair.Value, targetPair.Value);
            Console.ResetColor();
        }

        public static void PrintAsAdded(string action, KeyValuePair<int, string> targetPair)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t\t\t{2}\t\t\t{3}", action, targetPair.Key, "-", targetPair.Value);
            Console.ResetColor();
        }

        public static void PrintAsRemoved(string action, KeyValuePair<int, string> sourcePair)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t\t\t{2}\t\t\t{3}", action, sourcePair.Key, sourcePair.Value, "-");
            Console.ResetColor();
        }

        public static void PrintAsModified(string action, KeyValuePair<int, string> sourcePair, KeyValuePair<int, string> targetPair)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n{0}\t{1}\t\t\t{2}\t\t\t{3}", action, sourcePair.Key, sourcePair.Value, targetPair.Value);
            Console.ResetColor();
        }

        public static void PrintAsStringTypeIdPair(string action, KeyValuePair<string, string> pair)
        {
            Console.Write("\n{0}\t{1}\t\t\t{2}", action, pair.Key, pair.Value);
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
        /// Method prints data with int type keys (IDs).
        /// </summary>
        /// <param name="intSourceData"></param>
        /// <param name="intTargetData"></param>
        public static ResultCount PrintIntTypeIdData(Dictionary<int, string> intSourceData, Dictionary<int, string> intTargetData)
        {
            ResultCount count = default;

            foreach (var srcPair in intSourceData)
            {
                foreach (var trgPair in intTargetData)
                {
                    if (srcPair.Key > trgPair.Key)
                    {
                        continue;
                    }

                    if (srcPair.Key == trgPair.Key)
                    {
                        if (srcPair.Value == trgPair.Value)
                        {
                            PrintAsUnchanged("U", srcPair, trgPair);
                            count.unchanged++;
                            break;
                        }
                        else if (srcPair.Value != trgPair.Value
                            && intSourceData.ContainsKey(trgPair.Key)
                            && intTargetData.ContainsKey(srcPair.Key))
                        {
                            PrintAsModified("M", srcPair, trgPair);
                            count.modified++;
                            break;
                        }
                    }

                    else if (intSourceData.ContainsKey(trgPair.Key) && !intTargetData.ContainsKey(srcPair.Key))
                    {
                        PrintAsRemoved("R", srcPair);
                        count.removed++;
                        break;
                    }

                    else if (!intSourceData.ContainsKey(trgPair.Key) && intTargetData.ContainsKey(srcPair.Key))
                    {
                        PrintAsAdded("A", srcPair);
                        count.added++;
                    }
                }
            }
            return count;
        }

        public static void PrintComparisonResultsSummary(ResultCount count)
        {
            Console.WriteLine("U:{0} M:{1} R:{2} A:{3}",
                              count.unchanged, count.modified,
                              count.removed, count.added);
        }
    }
}
