using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    interface IDeviceInfoAction
    {
        void View(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string sourcePath, string targetPath);
    }
}
