using ParameterComparison.src.CLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.Mappers
{
    public class DeviceInfoMapper
    {

        public Dictionary<string, string> MapSource(DeviceInfoModel model)
        {
            return model.CreateSource();
        }
        public Dictionary<string, string> MapTarget(DeviceInfoModel model)
        {
            return model.CreateTarget();
        }

        public void Print(Dictionary<string, string> data, string path)
        {
            Printers.PrintDeviceConfigInfo(data.GetStringTypeKeys(), path);
        }
    }
}
