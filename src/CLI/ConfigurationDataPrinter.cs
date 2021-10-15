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

        public static ConsoleColor SetBackgroundColor(ComparedParam pair)
        {
            return pair.Action switch
            {
                "U" => Console.BackgroundColor = ConsoleColor.Gray,
                "M" => Console.BackgroundColor = ConsoleColor.Yellow,
                "R" => Console.BackgroundColor = ConsoleColor.Red,
                "A" => Console.BackgroundColor = ConsoleColor.Green,
                _ => Console.BackgroundColor,
            };
        }

    }
}
