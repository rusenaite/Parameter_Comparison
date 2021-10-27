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
            string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                              DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

            if (ConfigurationComparison.ContainsKeys(SourceData.GetStringTypeKeys(), keys))
            {
                return SourceData.GetStringTypeKeys();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// Method creates target device information model - dictionary of device information.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> If creation went well, returns dictionary of device information, otherwise - 
        /// throws an exception. </returns>
        public Dictionary<string, string> CreateTarget()
        {
            string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                              DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

            if (ConfigurationComparison.ContainsKeys(TargetData.GetStringTypeKeys(), keys))
            {
                return TargetData.GetStringTypeKeys();
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
