using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class DeviceInfo
    {
        public const string configVersion = "ConfigurationVersion";
        public const string hwVersion = "HwVersion";
        public const string title = "Title";
        public const string minConfiguration = "MinConfigurationVersion";
        public const string fmType = "FmType";
        public const string specId = "SpecId";

        public string ConfigurationVersion { get => configVersion; }
        public string HwVersion { get => hwVersion; }
        public string Title { get => title; }
        public string MinConfigurationVersion { get => minConfiguration; }
        public string FmType { get => fmType; }
        public string SpecId { get => specId; }
    }
}
