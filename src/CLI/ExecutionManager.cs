using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ExecutionManager
    {
        public void StartProgram()
        {
            IUIPrinter printer = new UIPrinter();
            printer.PrintMainMenu();
        }

        public void MakeAction(int userChoice)
        {

        }

    }
}
