using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class InputManager
    {
        public const int minOption = 0;
        public const int maxOption = 4;

        public static void GetActionChoice()
        {
            string userChoice = Console.ReadLine();

            if (IsValidInt(userChoice))
            {
                int choice = Int32.Parse(userChoice);
                MakeAction(choice);
            }
            else
            {
                Console.Error.Write("Error: Unavailable choice selected.");
            }
        }

        public static void MakeAction(int userChoice)
        {
            switch(userChoice)
                {
                    case 0:
                        System.Environment.Exit(1);
                        break;
                    case 1:
                        IConfigFilePrinter configPrinter = new ConfigurationComparison();
                        //configPrinter.PrintParametersList();
                        break;
                    case 2:
                        //
                        break;
                    case 3:
                        //
                        break;
                    case 4:
                        //
                        break;
                }
        }

        public static bool IsValidInt(string option)
        {
            return int.TryParse(option, out int choice) && (minOption <= choice && choice <= maxOption);
        }
    }
}
