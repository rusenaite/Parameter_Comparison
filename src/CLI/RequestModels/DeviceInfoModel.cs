using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.RequestModels
{
    public class DeviceInfoModel : RequestModel
    {
        public override List<ComparedParam> Viewer()
        {
            string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                              DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

            if (ContainsKeys(sourceData.GetStringTypeKeys(), keys) && ContainsKeys(targetData.GetStringTypeKeys(), keys))
            {
                Dictionary<string, string> sourceData = 
            }
        }
    }
}
