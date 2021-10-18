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
        const int MinOption = 0;
        const int MaxOption = 4;

        public const string LetterFilter = "^[AMRU]*$";
        public const string IdFilter = @"^\d+$";

        /// <summary>
        /// Method gets valid user input of desired action to take selected from menu.
        /// </summary>
        /// <returns> If entered input is valid, returns chosen action number, otherwise - 
        /// returns a default action number. </returns>
        public int GetActionChoice()
        {
            string choice = Console.ReadLine();

            while (!IsValidIntInRange(choice))
            {
                Console.Error.Write("[ Error ]: Unavailable choice entered. Re-enter your choice: ");
                choice = Console.ReadLine();
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

        /// <summary>
        /// Method returns whether string is an integer in [ minOption ; maxOption ] range.
        /// </summary>
        /// <param name="option"></param>
        /// <returns> If string is valid - returns true, otherwise - returns false. </returns>
        public static bool IsValidIntInRange(string option)
        {
            return int.TryParse(option, out int choice) && MinOption <= choice && choice <= MaxOption;
        }

    }
}
