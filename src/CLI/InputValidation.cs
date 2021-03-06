using ParameterComparison.src.CLI.Controllers;
using System;
using System.Text.RegularExpressions;

namespace ParameterComparison.src.CLI
{
    class InputValidation
    {
        public const string IdFilter = @"^\d+$";
        public const string ActionFilter = "^[0-3]$";

        /// <summary>
        /// Method gets valid user input of desired action to take selected from menu.
        /// </summary>
        /// <returns> If entered input is valid, returns chosen action number, otherwise - 
        /// returns a default action number. </returns>
        public static int GetActionChoice(string regex)
        {
            string choice = MainController.ReadLine();
            while (true)
            {
                Match match = Regex.Match(choice, regex, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    break;
                }
                else
                {
                    MainController.PrintInputError();
                    choice = MainController.ReadLine();
                }
            }

            int userChoice = int.Parse(choice);
            return userChoice;
        }

        /// <summary>
        /// Method gets valid selected user input of entered values.
        /// </summary>
        /// <returns> If entered input is valid, returns entered string, 
        /// otherwise - returns an empty string. </returns>
        public static string GetFilter(string regex)
        {
            string input = MainController.ReadLine();

            while (true)
            {
                Match match = Regex.Match(input, regex, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    break;
                }
                else
                {
                    MainController.PrintInputError();
                    input = MainController.ReadLine();
                }
            }

            return input;
        }

    }
}
