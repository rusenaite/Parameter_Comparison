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

        private readonly Dictionary<string, string> sourceData;
        private readonly Dictionary<string, string> targetData;

        public InputManager(Dictionary<string, string> srcData, Dictionary<string, string> trgData)
        {
            sourceData = srcData;
            targetData = trgData;
        }

        public void StartProgram()
        {
            InterfacePrinter printer = new InterfacePrinter();
            InterfacePrinter.PrintMainMenu();

            GetActionChoice();
        }

        public void GetActionChoice()
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
                MakeAction(choice);
            }
        }

        public void GetId()
        {
            string userInput = Console.ReadLine();

            while (!IsInt(userInput))
            {
                Console.Error.Write("Error: Unavailable choice entered. Re-enter your choice: ");
                userInput = Console.ReadLine();
            }

            if (IsInt(userInput))
            {
                int choice = Int32.Parse(userInput);
                IConfigFilePrinter configPrinter = new ConfigurationComparison();
                configPrinter.ViewFilteredParameters(sourceData, targetData, userInput);
            }
        }

        public void GetLetter()
        {
            string userInput = Console.ReadLine();

            bool result = userInput.Any(x => char.IsUpper(x));

            while (!result)
            {
                Console.Error.Write("Error: Unavailable input entered. Re-enter your input: ");
                userInput = Console.ReadLine();
            }

            if (result)
            {
                IConfigFilePrinter configPrinter = new ConfigurationComparison();
                configPrinter.ViewParamByComparisonResult(sourceData, targetData, userInput);
            }

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
                        GetId();
                    break;
                    case 3:
                        InterfacePrinter printer = new();
                        InterfacePrinter.PrintComparisonResultChoices();
                        GetLetter();
                        break;
                }
        }

    }
}
