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
            var printer = new UIPrinter();
            var inputValidator = new InputValidation();

            printer.ReadData(sourcePath, targetPath);

            int requestedAction = printer.GetActionRequest(inputValidator);

            printer.PrintRequest(selectedModel);

            IDeviceInfoAction deviceInfoViewer = new DeviceInfoViewer();
            var parametersListViewer = new ParametersListViewer();
            IAction comparisonResultsSummaryViewer = new CompResultsSummaryViewer();
            IFilteringAction filteredParamByIdViewer = new FilteredParamViewer();
            IFilteringAction filterParamByCompResultViewer = new FilterParamByCompResultViewer();

            UIPrinter.PrintMainMenu();

            int actionChoice = inputValidator.GetActionChoice(InputValidation.ActionFilter);
        }
    }
}
