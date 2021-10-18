using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParameterComparison
{
    class InputValidation : IUserInput
    {

        public const string LetterFilter = "^[AMRU]*$";
        public const string IdFilter = @"^\d+$";
        public const string ActionFilter = "^[0-3]$";

        /// <summary>
        /// Method gets valid user input of desired action to take selected from menu.
        /// </summary>
        /// <returns> If entered input is valid, returns chosen action number, otherwise - 
        /// returns a default action number. </returns>
        public int GetActionChoice(string regex)
        {
            string choice = Console.ReadLine();

            while (true)
            {
                Match match = Regex.Match(choice, regex, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    break;
                }
                else
                {
                    Console.Error.Write("[ Error ]: Unavailable choice entered. Re-enter your choice: ");
                    choice = Console.ReadLine();
                }
            }

            int userChoice = Int32.Parse(choice);
            return userChoice;
        }

        /// <summary>
        /// Method gets valid selected user input of entered values.
        /// </summary>
        /// <returns> If entered input is valid, returns entered string, 
        /// otherwise - returns an empty string. </returns>
        public string GetFilter(string regex)
        {
            string input = Console.ReadLine();

            while (true)
            {
                Match match = Regex.Match(input, regex, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    break;
                }
                else
                {
                    Console.Error.Write("[ Error ]: Unavailable input entered. Re-enter your input: ");
                    input = Console.ReadLine();
                }
            }

            return input;
        }

    }
}
