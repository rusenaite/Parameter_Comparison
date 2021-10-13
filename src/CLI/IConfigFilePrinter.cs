using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    interface IConfigFilePrinter
    {
        void ViewDeviceConfigInfo(Dictionary<string, string> data, string path);
        void ViewParameterList(Dictionary<string, string> sourceData, Dictionary<string, string> targetData);
        void ViewComparisonResultsSummary(Dictionary<string, string> sourceData, Dictionary<string, string> targetData);
        void ViewFilteredParameters(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string filter);
        void ViewParamByComparisonResult(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string choice);
    }
}
