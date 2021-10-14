using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ParametersListViewer : ConfigurationComparison, IAction
    {
        /// <summary>
        /// Method prints parameter list - paramater ID, value and comparison result of
        /// 2 configuration files.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public void View(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            List<ComparedParam> comparedData = CompareConfig(sourceData.GetIntTypeKeys(), targetData.GetIntTypeKeys());

            ConfigurationDataPrinter.PrintColumnNames();
            PrintStringTypeIdData(sourceData.GetStringTypeKeys().RemovedDeviceInfo(), targetData.GetStringTypeKeys().RemovedDeviceInfo());
            ConfigurationDataPrinter.PrintComparedData(comparedData);
        }

        /// <summary>
        /// Method seperately prints data with string type keys (IDs) (as it is not included
        /// to the parameter comparison).
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public static void PrintStringTypeIdData(Dictionary<string, string> stringSourceData, Dictionary<string, string> stringTargetData)
        {
            foreach (var srcPair in stringSourceData)
            {
                PrintAsStringTypeIdPair("-", srcPair);
            }

            foreach (var trgPair in stringTargetData)
            {
                PrintAsStringTypeIdPair("-", trgPair);
            }
        }

        /// <summary>
        /// Method prints string type provided KeyValuePair type ID pair.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="pair"></param>
        public static void PrintAsStringTypeIdPair(string action, KeyValuePair<string, string> pair)
        {
            Console.Write("\n{0}\t{1}\t\t\t\t\t{2}", action, pair.Key, pair.Value);
        }

    }
}
