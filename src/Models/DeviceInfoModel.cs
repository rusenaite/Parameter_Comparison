using ParameterComparison.src.ConfigDataProcessing;
using System;
using System.Collections.Generic;

namespace ParameterComparison.src.Models
{
    public class DeviceInfoModel : IRequestModel
    {
        public Dictionary<string, string> SourceDeviceInfo { get; set; }
        public Dictionary<string, string> TargetDeviceInfo { get; set; }
        public string SourcePath { get; set; }
        public string TargetPath { get; set; }
    }
}
