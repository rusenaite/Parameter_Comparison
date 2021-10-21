using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    interface IFilteringAction
    {
        void View(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string s);
    }
}
