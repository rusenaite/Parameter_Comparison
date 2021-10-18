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

            Dictionary<string, string> sourceData = FileReader.ReadGZippedFiles(sourcePath);
            Dictionary<string, string> targetData = FileReader.ReadGZippedFiles(targetPath);

            IUIPrinter printer = new UIPrinter();
            IUserInput inputValidator = new InputValidation();

            IDeviceInfoAction deviceInfoViewer = new DeviceInfoViewer();
            IAction parametersListViewer = new ParametersListViewer();
            IAction comparisonResultsSummaryViewer = new CompResultsSummaryViewer();
            IFilteringAction filteredParamByIdViewer = new FilteredParamViewer();
            IFilteringAction filterParamByCompResultViewer = new FilterParamByCompResultViewer();

            printer.PrintMainMenu();

            int actionChoice = inputValidator.GetActionChoice(InputValidation.ActionFilter);

            deviceInfoViewer.View(sourceData, targetData, sourcePath, targetPath); 

            switch (actionChoice)
            {
                case 0:
                    parametersListViewer.View(sourceData, targetData);
                    break;
                case 1:
                    comparisonResultsSummaryViewer.View(sourceData, targetData);
                    break;
                case 2:
                    string idFilter = inputValidator.GetFilter(InputValidation.IdFilter);

                    filteredParamByIdViewer.View(sourceData, targetData, idFilter);
                    break;
                case 3:
                    printer.PrintComparisonResultChoices();
                    string stateFilter = inputValidator.GetFilter(InputValidation.LetterFilter);

                    filterParamByCompResultViewer.View(sourceData, targetData, stateFilter);
                    break;
            }
        }
    }
}
