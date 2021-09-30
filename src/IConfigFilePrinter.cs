using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    interface IConfigFilePrinter
    {
        void PrintDeviceConfigInfo(Dictionary<string, string> data, string path);
        void PrintConfigData(Dictionary<string, string> sourceData, Dictionary<string, string> targetData);
        void ViewParameterList(Dictionary<string, string> sourceData, Dictionary<string, string> targetData);
    }
}
