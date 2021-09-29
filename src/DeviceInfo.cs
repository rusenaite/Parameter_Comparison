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
        public const string minConfiguration = "MinConfiguratorVersion";
        public const string fmType = "FmType";
        public const string specId = "SpecId";

        public DeviceInfo(Dictionary<string, string> sourceFile)
        {
            SourceFile = sourceFile;
        }

        public ConfigData ConfigurationVersion { get; set; }
        public ConfigData HwVersion { get; set; }
        public ConfigData Title { get; set; }
        public ConfigData MinConfigurationVersion { get; set; }
        public ConfigData FmType { get; set; }
        public ConfigData SpecId { get; set; }
        public Dictionary<string, string> SourceFile { get; }
    }
}
