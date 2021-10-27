using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.Models
{
    public interface RequestModel
    {
        private static Dictionary<string, string> sourceData;
        private static Dictionary<string, string> targetData;

        //public abstract List<ComparedParam> Create(Dictionary<string, string> sourceData, Dictionary<string, string> targetData);
    }
}
