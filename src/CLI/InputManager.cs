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

        private Dictionary<string, string> sourceData;
        private Dictionary<string, string> targetData;

        private string sourcePath;
        private string targetPath;

        private readonly string defaultLetter = "U";
        private readonly int defaultAction = 0;

        public InputManager(string srcPath, string trgPath)
        {
            sourcePath = srcPath;
            targetPath = trgPath;
            SetData(sourcePath, targetPath);
        }

        public void SetData(string srcPath, string trgPath)
        {
            sourceData = FileReader.ReadGZippedFiles(srcPath);
            targetData = FileReader.ReadGZippedFiles(trgPath);
        }

        public void StartProgram()
        {
            InterfacePrinter printer = new InterfacePrinter();
            InterfacePrinter.PrintMainMenu();

            int choice = GetActionChoice();
            MakeAction(choice);
        }

        /// <summary>
        /// Method gets user input of desired action to take selected from menu.
        /// </summary>
        /// <returns> If reading went well, returns chosen action number, otherwise - 
        /// returns a default action number. </returns>
        public int GetActionChoice()
        {
            string userChoice = Console.ReadLine();

            while (!IsValidIntInRage(userChoice))
            {
                Console.Error.Write("Error: Unavailable choice entered. Re-enter your choice: ");
                userChoice = Console.ReadLine();
            }

            if (IsValidIntInRage(userChoice))
            {
                int choice = Int32.Parse(userChoice);
                return choice;
            }
            return defaultAction;
        }

        /// <summary>
        /// Method gets user input of entered ID value (or a part of it) to filter 
        /// compared data with integer type ID values.
        /// </summary>
        /// <returns> If reading went well, returns entered value, otherwise - an empty
        /// string. </returns>
        public static string GetIdFilter()
        {
            string userInput = Console.ReadLine();

            while (!IsInt(userInput))
            {
                Console.Error.Write("Error: Unavailable choice entered. Re-enter your choice: ");
                userInput = Console.ReadLine();
            }

            if (IsInt(userInput))
            {
                return userInput;
            }

            return "";
        }

        /// <summary>
        /// Method gets user input of chosen desired comparison result filter.
        /// </summary>
        /// <returns> If reading went well, returns desired user entered letter, 
        /// otherwise - returns a default letter. </returns>
        public string GetLetterFilter()
        {
            bool incorrect = true;

            while (incorrect)
            {
                string userInput = Console.ReadLine();

                while (!IsRequiredLetter(userInput))
                {
                    Console.Error.Write("Error: Unavailable input entered. Re-enter your input: ");
                    userInput = Console.ReadLine();
                }

                if (IsRequiredLetter(userInput))
                {
                    return userInput;
                }
            }

            return defaultLetter;
        }

        /// <summary>
        /// Method validates whether passed string is one of required capital letters
        /// used to choose comparison data filter (letters: U, M, R, A).
        /// </summary>
        /// <param name="input"></param>
        /// <returns> If string is valid returns true, otherwise - returns false. </returns>
        public static bool IsRequiredLetter(string input)
        {
            return string.Equals(input, "U") | string.Equals(input, "M") | string.Equals(input, "R") | string.Equals(input, "A");
        }

        /// <summary>
        /// Method returns whether string is an integer in [ minOption ; maxOption ] range.
        /// </summary>
        /// <param name="option"></param>
        /// <returns> If string is valid returns true, otherwise - returns false. </returns>
        public static bool IsValidIntInRage(string option)
        {
            return int.TryParse(option, out int choice) && (minOption <= choice && choice <= maxOption);
        }

        /// <summary>
        /// Method returns whether string is an integer;
        /// </summary>
        /// <param name="option"></param>
        /// <returns> If string is valid returns true, otherwise - returns false. </returns>
        public static bool IsInt(string option)
        {
            return int.TryParse(option, out int choice);
        }

        public void MakeAction(int userChoice)
        {
            IConfigFilePrinter configPrinter = new ConfigurationComparison();
            //configPrinter.ViewDeviceConfigInfo(sourceData, sourcePath);
            //configPrinter.ViewDeviceConfigInfo(targetData, targetPath);

            switch (userChoice)
                {
                    case 0:
                        configPrinter.ViewParameterList(sourceData, targetData);
                        break;
                    case 1:
                        configPrinter.ViewComparisonResultsSummary(sourceData, targetData);
                        break;
                    case 2:
                        string idFilter = GetIdFilter();
                        configPrinter.ViewFilteredParameters(sourceData, targetData, idFilter);

                    break;
                    case 3:
                        InterfacePrinter printer = new();
                        InterfacePrinter.PrintComparisonResultChoices();

                        string letterFilter = GetLetterFilter();
                        configPrinter.ViewParamByComparisonResult(sourceData, targetData, letterFilter);
                    break;
                }
        }

    }
}
