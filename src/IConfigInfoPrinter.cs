using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    interface IConfigChangesPrinter
    {
        void PrintDeviceConfigInfo(Dictionary<string, string> sourceFile, Dictionary<string, string> targetFile);
    }
}
