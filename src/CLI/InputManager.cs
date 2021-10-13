using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class InputManager
    {
        public void StartProgram()
        {
            IActionPrinter printer = new ActionViewer();
            printer.ViewDeviceConfigInfo(sourceData, sourcePath);
            printer.ViewDeviceConfigInfo(targetData, targetPath);

            printer.PrintMainMenu();

            int choice = GetActionChoice();
            MakeAction(choice);
        }

        public void MakeAction(int userChoice)
        {
            IActionPrinter printer = new ActionViewer();

            switch (userChoice)
            {
                case 0:
                    printer.ViewParameterList(sourceData, targetData);
                    break;
                case 1:
                    printer.ViewComparisonResultsSummary(sourceData, targetData);
                    break;
                case 2:
                    string idFilter = GetIdFilter();
                    printer.ViewFilteredParameters(sourceData, targetData, idFilter);
                    break;
                case 3:
                    printer.PrintComparisonResultChoices();

                    string letterFilter = GetLetterFilter();
                    printer.ViewParamByComparisonResult(sourceData, targetData, letterFilter);
                    break;
            }
        }

    }
}
