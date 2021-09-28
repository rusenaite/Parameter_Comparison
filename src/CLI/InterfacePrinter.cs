using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class InterfacePrinter : IMenuPrinter
    {
        public void PrintMainMenu()
        {
            Console.WriteLine(@"Select action You would like to perform:" +
                                "\n[0] Exit program" +
                                "\n[1] View parameter's list" +
                                "\n[2] Summary of results" +
                                "\n[3] Find/filtrate parameters by ID" +
                                "\n[4] Filtrate parameters by comparison result" +
                                "\n\nEnter your choice:");
        }
    }
}
