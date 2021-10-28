using ParameterComparison.src.CLI.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ParameterComparison
{
    class Program
    {
        public const string sourcePath = "C:/Users/raust/source/repos/ParameterComparison/data_samples/FMB001-default.cfg";
        public const string targetPath = "C:/Users/raust/source/repos/ParameterComparison/data_samples/FMB920-modified.cfg";
        static void Main()
        {
            try
            {
                var printer = new MainController(sourcePath, targetPath);
                var inputValidator = new InputValidation();

                printer.PrintMainMenu();

                var requestedAction = printer.GetAction(inputValidator);

                var request = printer.GetRequestedModel(requestedAction);

                printer.PrintRequest(request);

            }
            catch (NullReferenceException err)
            {
                Console.WriteLine(err.Message);
            }
            catch (NotImplementedException err)
            {
                Console.WriteLine(err.Message);
            }

        }
    }
}
