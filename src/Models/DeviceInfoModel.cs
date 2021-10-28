using ParameterComparison.src.ConfigDataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.Models
{
    public class DeviceInfoModel : IRequestModel
    {
        public static Dictionary<string, string> SourceData;
        public static Dictionary<string, string> TargetData;

        public readonly string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                                          DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

        /// <summary>
        /// Constructor assigns passed source and target data dictionary collections to class fields.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public DeviceInfoModel(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            SourceData = sourceData;
            TargetData = targetData;
        }

        /// <summary>
        /// Method creates source device information model - dictionary of device information.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If creation went well, returns dictionary of device information, otherwise - 
        /// throws an exception. </returns>
        public Dictionary<string, string> CreateSource()
        {
            if (!ConfigurationComparison.ContainsKeys(SourceData.GetStringTypeKeys(), keys))
            {
                throw new NullReferenceException();
            }

            return SourceData.GetStringTypeKeys();
        }

        /// <summary>
        /// Method creates target device information model - dictionary of device information.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If creation went well, returns dictionary of device information, otherwise - 
        /// throws an exception. </returns>
        public Dictionary<string, string> CreateTarget()
        {
            if (!ConfigurationComparison.ContainsKeys(TargetData.GetStringTypeKeys(), keys))
            {
                throw new NullReferenceException();
            }

            return TargetData.GetStringTypeKeys();
        }
    }
}
