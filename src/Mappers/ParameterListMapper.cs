using ParameterComparison.src.Models;
using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;
using System.Linq;

namespace ParameterComparison.src.CLI.Mappers
{
    public class ParameterListMapper
    {
        public Dictionary<string, string> SourceData;
        public Dictionary<string, string> TargetData;

        /// <summary>
        /// Constructor assigns passed source and target data dictionary collections to class fields.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public ParameterListMapper(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            SourceData = sourceData;
            TargetData = targetData;
        }

        public ParameterListModel Map(ParameterListModel model)
        {
            model.ParameterList = ConfigurationComparison.CompareConfig(SourceData, TargetData);
            return model;
        }
    }
}
