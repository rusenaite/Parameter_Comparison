using ParameterComparison.src.ConfigDataProcessing;
using ParameterComparison.src.Models;
using System.Collections.Generic;

namespace ParameterComparison.src.CLI.Mappers
{
    public class DeviceInfoMapper
    {
        public Dictionary<string, string> SourceData;
        public Dictionary<string, string> TargetData;
        public string sourcePath;
        public string targetPath;

        public readonly string[] keys = { DeviceInfo.configVersion, DeviceInfo.hwVersion, DeviceInfo.title,
                                          DeviceInfo.minConfiguration, DeviceInfo.fmType, DeviceInfo.specId };

        /// <summary>
        /// Constructor assigns passed source and target data dictionary collections to class fields.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public DeviceInfoMapper(Dictionary<string, string> srcData, Dictionary<string, string> trgData, string srcPath, string trgPath)
        {
            SourceData = srcData;
            TargetData = trgData;
            sourcePath = srcPath;
            targetPath = trgPath;
        }

        public DeviceInfoModel Map(DeviceInfoModel model)
        {
            model.SourceDeviceInfo = SourceData.GetStringTypeKeys();
            model.TargetDeviceInfo = TargetData.GetStringTypeKeys();
            model.SourcePath = sourcePath;
            model.TargetPath = targetPath;

            return model;
        }
    }
}
