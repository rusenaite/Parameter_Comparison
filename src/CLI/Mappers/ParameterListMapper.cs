using ParameterComparison.src.CLI.Models;
using ParameterComparison.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.Mappers
{
    public class ParameterListMapper
    {
        /// <summary>
        /// Method creates passed model and returns result.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> If creation went well, returns a list of parameters, otherwise - an empty list. </returns>
        public List<ComparedParam> Map(ParameterListModel model)
        {
            return model.Create();
        }

        /// <summary>
        /// Method prints a list of parameters.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <param name="result"></param>
        public void Print(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, List<ComparedParam> result)
        {
            Printers.PrintComparedData(result);
            Printers.PrintStringTypeIdData(sourceData.GetStringTypeKeys().RemovedDeviceInfo(), targetData.GetStringTypeKeys().RemovedDeviceInfo());
        }

    }
}
