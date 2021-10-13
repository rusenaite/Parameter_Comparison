using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI
{
    public class UIPrinter : IUIPrinter
    {
        /// <summary>
        /// Method prints main menu with actions for user to choose from.
        /// </summary>
        public void PrintMainMenu()
        {
            Console.WriteLine(@"Select action You would like to perform:" +
                                "\n[0] View parameter's list" +
                                "\n[1] Summary of results" +
                                "\n[2] Filter parameters by entered ID" +
                                "\n[3] Filter parameters by comparison result" +
                                "\n\nEnter your choice (number from interval [ 0 ; 3 ]:");
        }

        /// <summary>
        /// Method prints comparison result choices for user to choose from.
        /// </summary>
        public void PrintComparisonResultChoices()
        {
            Console.WriteLine(@"Select action You would like to perform:" +
                                "\n[U] Unchanged" +
                                "\n[A] Added" +
                                "\n[M] Modified" +
                                "\n[R] Removed" +
                                "\n\nEnter your choice (one of 4 capital letters):");
        }
    }
}
