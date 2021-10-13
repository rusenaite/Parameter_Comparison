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
        /// Method prints column names for configuration data.
        /// </summary>
        public static void PrintColumnNames()
        {
            Console.Write("\n\n\n{0}\t{1}\t\t\t{2}\t\t\t{3}", "Status", "ID", "Source Value", "Target Value");
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
