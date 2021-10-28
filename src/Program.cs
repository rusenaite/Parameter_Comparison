using ParameterComparison.src.CLI;
using ParameterComparison.src.CLI.Controllers;
using System;

namespace ParameterComparison.src
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

                printer.PrintMainMenu();

                var requestedAction = printer.GetAction();

                var request = printer.GetRequestedModel(requestedAction);

                var requestedDeviceInfoModel = printer.RequestDeviceInfo();

                printer.PrintRequest(requestedDeviceInfoModel);
                printer.PrintRequest(request);

                Console.ReadLine();
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
