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
                ParamAction.Unmodified => Console.BackgroundColor = ConsoleColor.Gray,
                ParamAction.Modified => Console.BackgroundColor = ConsoleColor.Yellow,
                ParamAction.Removed => Console.BackgroundColor = ConsoleColor.Red,
                ParamAction.Added => Console.BackgroundColor = ConsoleColor.Green,
                _ => Console.BackgroundColor,
            };
        }

    }
}
