using ParameterComparison.src.CLI.Models;
using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;

namespace ParameterComparison.src.CLI.Mappers
{
    public class DeviceInfoMapper
    {
        /// <summary>
        /// Method creates passed source device information model and returns result.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> If creation went well, returns a dictionary of parameters, otherwise - an empty dictionary. </returns>
        public Dictionary<string, string> MapSource(DeviceInfoModel model)
        {
            return model.CreateSource();
        }

        /// <summary>
        /// Method creates passed target device information model and returns result.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> If creation went well, returns a dictionary of parameters, otherwise - an empty dictionary. </returns>
        public Dictionary<string, string> MapTarget(DeviceInfoModel model)
        {
            return model.CreateTarget();
        }

        /// <summary>
        /// Method prints device information.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public void Print(Dictionary<string, string> data, string path)
        {
            Printers.PrintDeviceConfigInfo(data.GetStringTypeKeys(), path);
        }
    }
}
