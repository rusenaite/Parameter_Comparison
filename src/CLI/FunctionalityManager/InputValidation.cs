using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    class InputValidation : IUserInput
    {
        public const int MinOption = 0;
        public const int MaxOption = 4;

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
        /// Method gets valid user input of entered integer type ID value (or a part of it).
        /// </summary>
        /// <returns> If entered input is valid, returns entered value, otherwise - an empty
        /// string. </returns>
        public string GetIdFilter()
        {
            string userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out _))
            {
                Console.Error.Write("[ Error ]: Unavailable choice entered. Re-enter your choice: ");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        /// <summary>
        /// Method gets valid user input of chosen desired comparison result filter.
        /// </summary>
        /// <returns> If entered input is valid, returns entered letter, 
        /// otherwise - returns a default letter. </returns>
        public string GetLetterFilter()
        {
            string input = Console.ReadLine();

            while (!string.Equals(input, "U") | !string.Equals(input, "M") | !string.Equals(input, "R") | !string.Equals(input, "A"))
            {
                Console.Error.Write("[ Error ]: Unavailable input entered. Re-enter your input: ");
                input = Console.ReadLine();
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
