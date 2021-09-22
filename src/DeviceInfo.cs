using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class DeviceInfo
    {
        private const string configVersion = "ConfigurationVersion";
        private const string hwVersion = "HwVersion";
        private const string title = "Title";
        private const string minConfiguration = "MinConfiguratorVersion";
        private const string fmType = "FmType";
        private const string specId = "SpecId";

        public ConfigData ConfigurationVersion { get; set; }
        public ConfigData HwVersion { get => HwVersion; set => HwVersion.Id = hwVersion; }
        public ConfigData Title { get => Title; set => Title.Id = title; }
        public ConfigData MinConfigurationVersion { get => MinConfigurationVersion; set => MinConfigurationVersion.Id = minConfiguration; }
        public ConfigData FmType { get => FmType; set => FmType.Id = fmType; }
        public ConfigData SpecId { get => SpecId; set => SpecId.Id = specId; }

    }
}
