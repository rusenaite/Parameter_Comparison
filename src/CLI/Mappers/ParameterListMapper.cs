using ParameterComparison.src.CLI.Models;
using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;
using System.Linq;

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
            return model.GetResult();
        }

        /// <summary>
        /// Method prints a list of parameters.
        /// </summary>
        /// <param name="result"></param>
        public void Print(List<ComparedParam> result)
        {
            Printers.PrintComparedData(result.GetIntTypeKeys());

            if (result.GetStringTypeKeys().RemovedDeviceInfo().Any())
            {
                Printers.PrintStringTypeIdData(result.GetStringTypeKeys().RemovedDeviceInfo());
            }
        }

    }
}
