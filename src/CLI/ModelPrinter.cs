using ParameterComparison.src.ConfigDataProcessing;
using ParameterComparison.src.Models;
using System.Linq;

namespace ParameterComparison.src.CLI
{
    public class ModelPrinter
    {
        /// <summary>
        /// Method prints device information.
        /// </summary>
        /// <param name="model"></param>
        public static void PrintDeviceInfoModel(DeviceInfoModel model)
        {
            Printers.PrintDeviceConfigInfo(model.SourceDeviceInfo, model.SourcePath);
            Printers.PrintDeviceConfigInfo(model.TargetDeviceInfo, model.TargetPath);
        }

        /// <summary>
        /// Method prints a list of parameters.
        /// </summary>
        /// <param name="model"></param>
        public static void PrintParameterListModel(ParameterListModel model)
        {
            Printers.PrintComparedData(model.ParameterList);
        }

        /// <summary>
        /// Method prints a list of filtered compared parameters.
        /// </summary>
        /// <param name="model"></param>
        public static void PrintFilteredDataByComparisonResultModel(ComparisonResultFilterModel model)
        {
            Printers.PrintComparedData(model.FilteredDataByComparisonResult);
        }

        /// <summary>
        /// Method prints a list of filtered data by ID.
        /// </summary>
        /// <param name="model"></param>
        public static void PrintFilteredDataByIdModel(IdFilterModel model)
        {
            Printers.PrintComparedData(model.FilteredDataById);
        }

        /// <summary>
        /// Method prints parameter comparison results.
        /// </summary>
        /// <param name="model"></param>
        public static void PrintResultSummaryModel(ResultsSummaryModel model)
        {
            Printers.PrintComparisonResultsSummary(model.ResultCount);
        }

    }
}
