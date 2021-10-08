using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ActionViewer : ConfigurationComparison, IConfigFilePrinter
    {
        /// <summary>
        /// Method prints device configuration information.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public void ViewDeviceConfigInfo(Dictionary<string, string> data, string path)
        {
            data.GetStringTypeKeys();

            DeviceInfo devInfo = new DeviceInfo();
            string[] keys = { devInfo.ConfigurationVersion, devInfo.HwVersion, devInfo.Title,
                              devInfo.MinConfigurationVersion, devInfo.FmType, devInfo.SpecId };

            if (ContainsKeys(data.GetStringTypeKeys(), keys))
            {
                PrintDeviceConfigInfo(data.GetStringTypeKeys(), path);
            }
        }

        /// <summary>
        /// Method prints parameter list - paramater ID, value and comparison result of
        /// 2 configuration files.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public void ViewParameterList(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            List<ComparedParam> comparedData = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            PrintColumnNames();
            PrintStringTypeIdData(sourceData.GetStringTypeKeys().RemovedDeviceInfo(), targetData.GetStringTypeKeys().RemovedDeviceInfo());
            PrintComparedData(comparedData);
        }

        /// <summary>
        /// Method allows to print comparison result summary.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public void ViewComparisonResultsSummary(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            List<ComparedParam> list = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            for (int i = 0; i < resultCount.Length; ++i)
            {
                resultCount[i].count = list.Where(pair => pair.Action == resultCount[i].result && pair.Action.Any()).Count();
            }

            PrintComparisonResultsSummary(resultCount);
        }

        /// <summary>
        /// Method prints filtered parameters by a given key value (filter).
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <param name="filter"></param>
        public void ViewFilteredParameters(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string filter)
        {
            List<ComparedParam> list = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());
            List<ComparedParam> foundParamList = SearchForValue(list, filter);

            PrintColumnNames();
            PrintComparedData(foundParamList);
        }

        /// <summary>
        /// Method allows printing parameters of selected comparison result.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <param name="choice"></param>
        public void ViewParamByComparisonResult(Dictionary<string, string> sourceData, Dictionary<string, string> targetData, string choice)
        {
            List<ComparedParam> list = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            PrintColumnNames();

            list.Where(pair => pair.Action == choice && pair.Action.Any()).ToList().ForEach(p =>
            {
                PrintComparedPair(p);
            });
        }
    }
}
