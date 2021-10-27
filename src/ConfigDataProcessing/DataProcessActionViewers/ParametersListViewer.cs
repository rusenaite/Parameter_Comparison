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
            List<ComparedParam> comparedData = CompareConfig(sourceData, targetData);

            Printers.PrintComparedData(comparedData);
            PrintStringTypeIdData(sourceData.GetStringTypeKeys().RemovedDeviceInfo(), targetData.GetStringTypeKeys().RemovedDeviceInfo());
        }

        /// <summary>
        /// Method seperately prints data with string type keys (IDs) (as it is not included
        /// to the parameter comparison).
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        public static void PrintStringTypeIdData(Dictionary<string, string> stringSourceData, Dictionary<string, string> stringTargetData)
        {
            stringSourceData.ToList().ForEach(pair =>
            {
                Console.Write("\n".PadRight(Columns.ColumnWidth) + pair.Key.PadRight(Columns.ColumnWidth) + pair.Value.PadRight(Columns.ColumnWidth));
            });

            stringTargetData.ToList().ForEach(pair =>
            {
                Console.Write("\n".PadRight(Columns.ColumnWidth) + pair.Key.PadRight(Columns.ColumnWidth) + pair.Value.PadRight(Columns.ColumnWidth));
            });
        }

    }
}
