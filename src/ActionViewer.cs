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
        /// Method prints main menu with actions for user to choose from.
        /// </summary>
        public void PrintMainMenu()
        {
            Console.WriteLine(@"Select action You would like to perform:" +
                                "\n[0] View parameter's list" +
                                "\n[1] Summary of results" +
                                "\n[2] Filter parameters by entered ID" +
                                "\n[3] Filter parameters by comparison result" +
                                "\n\nEnter your choice (number from interval [ 0 ; 3 ]:");
        }

        /// <summary>
        /// Method prints comparison result choices for user to choose from.
        /// </summary>
        public void PrintComparisonResultChoices()
        {
            Console.WriteLine(@"Select action You would like to perform:" +
                                "\n[U] Unchanged" +
                                "\n[A] Added" +
                                "\n[M] Modified" +
                                "\n[R] Removed" +
                                "\n\nEnter your choice (one of 4 capital letters):");
        }
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

            if (!foundParamList.Any())
            {
                Console.Error.WriteLine("\n\nCompared data does not contain parameters with entered ID value.");
            }
            else
            {
                PrintColumnNames();
                PrintComparedData(foundParamList);
            }
                
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

            List<ComparedParam> chosenList = list.Where(pair => pair.Action == choice && pair.Action.Any()).ToList();

            if (!chosenList.Any())
            {
                Console.Error.WriteLine("\n\nCompared data does not contain parameters with selected comparison result.");
            }
            else
            {
                PrintColumnNames();
                PrintComparedData(chosenList);
            }
        }
    }
}
