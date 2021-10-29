using ParameterComparison.src.CLI.Mappers;
using ParameterComparison.src.Models;
using System;
using System.Collections.Generic;

namespace ParameterComparison.src.CLI.Controllers
{
    public class MainController
    {
        private static string sourcePath;
        private static string targetPath;

        public Dictionary<string, string> SourceData { get; set; }
        public Dictionary<string, string> TargetData { get; set; }

        /// <summary>
        /// Constructor sets passed source and target paths.
        /// </summary>
        /// <param name="_sourcePath"></param>
        /// <param name="_targetPath"></param>
        public MainController(string _sourcePath, string _targetPath)
        {
            sourcePath = _sourcePath;
            targetPath = _targetPath;

            SourceData = FileReader.ReadGZippedFiles(sourcePath);
            TargetData = FileReader.ReadGZippedFiles(targetPath);
        }

        /// <summary>
        /// Method calls user input validation for entering action choice.
        /// </summary>
        /// <param name="inputValidator"></param>
        /// <returns> Calls method <see cref="InputValidation.GetActionChoice(string)"> GetActionChoice(string) </see> </returns>
        public int GetAction() => InputValidation.GetActionChoice(InputValidation.ActionFilter);

        /// <summary>
        /// Method calls user input validation for entering input of selected filter.
        /// </summary>
        /// <param name="inputValidator"></param>
        /// <param name="filter"></param>
        /// <returns> Calls method <see cref="InputValidation.GetFilter(string)"> GetFilter(string) </see> </returns>
        public static string GetFilter(string filter) => InputValidation.GetFilter(filter);

        /// <summary>
        /// Method returns requested model by passed action parameter.
        /// </summary>
        /// <param name="action"></param>
        /// <returns> If available action parameter passed, returns one of the request models,
        /// otherwise - throws an exception. </returns>
        public IRequestModel GetRequestedModel(int action)
        {
            return action switch
            {
                0 => new ParameterListModel(),
                1 => new ResultsSummaryModel(),
                2 => new IdFilterModel(),
                3 => new ComparisonResultFilterModel(),
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Method creates a device information model.
        /// </summary>
        /// <returns> New <see cref="DeviceInfoModel"> DeviceInfoModel(Dictionary, Dictionary) </see> </returns>
        public IRequestModel RequestDeviceInfo()
        {
            return new DeviceInfoModel();
        }

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
                                "\n\nEnter your choice (number from interval [ 0 ; 3 ]):");
        }

        /// <summary>
        /// Method prints comparison result choices for user to choose from.
        /// </summary>
        public static void PrintComparisonResultChoices()
        {
            Console.WriteLine(@"Select action You would like to perform:" +
                                "\n[0] Unchanged" +
                                "\n[1] Added" +
                                "\n[2] Modified" +
                                "\n[3] Removed" +
                                "\n\nEnter your choice (number from interval [ 0 ; 3 ]):");
        }

        /// <summary>
        /// Method reads input from standart input stream.
        /// </summary>
        /// <returns> Reading from input stream. </returns>
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Method prints error for incorrectly entered input.
        /// </summary>
        public static void PrintInputError()
        {
            Console.Error.Write("[ Error ]: Unavailable input entered. Re-enter your choice: ");
        }

        /// <summary>
        /// Method prints data action requested by a passed model.
        /// </summary>
        /// <param name="model"></param>
        public void PrintRequest(IRequestModel model)
        {
            switch (model)
            {
                case DeviceInfoModel:
                    PrintDeviceInfo((DeviceInfoModel)model);
                    break;
                case ParameterListModel:
                    PrintParametersList((ParameterListModel)model);
                    break;
                case IdFilterModel:
                    PrintFilteredByIdList((IdFilterModel)model);
                    break;
                case ComparisonResultFilterModel:
                    PrintFilteredByComparisonResultList((ComparisonResultFilterModel)model);
                    break;
                case ResultsSummaryModel:
                    PrintResultSummary((ResultsSummaryModel)model);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Method prints result summary.
        /// </summary>
        /// <param name="model"></param>
        private void PrintResultSummary(ResultsSummaryModel model)
        {
            var mapper = new ResultsSummaryMapper(SourceData, TargetData);
            var result = mapper.Map(model);
            ModelPrinter.PrintResultSummaryModel(result);
        }

        /// <summary>
        /// Method prints compared parameters list filtered by entered comparison result.
        /// </summary>
        /// <param name="model"></param>
        private void PrintFilteredByComparisonResultList(ComparisonResultFilterModel model)
        {
            var mapper = new FilteredDataByComparisonResultMapper(SourceData, TargetData);

            PrintComparisonResultChoices();
            var choice = GetFilter(InputValidation.ActionFilter);

            var result = mapper.Map(model, choice);
            ModelPrinter.PrintFilteredDataByComparisonResultModel(result);
        }

        /// <summary>
        /// Method prints compared parameter list filtered by enter ID (or a part of ID value).
        /// </summary>
        /// <param name="model"></param>
        public void PrintFilteredByIdList(IdFilterModel model)
        {
            var mapper = new FilteredDataByIdMapper(SourceData, TargetData);

            var id = GetFilter(InputValidation.IdFilter);

            var result = mapper.Map(model, id);
            ModelPrinter.PrintFilteredDataByIdModel(result);
        }

        /// <summary>
        /// Method prints device configuration information.
        /// </summary>
        /// <param name="model"></param>
        public void PrintDeviceInfo(DeviceInfoModel model)
        {
            var mapper = new DeviceInfoMapper(SourceData, TargetData, sourcePath, targetPath);
            var result = mapper.Map(model);
            ModelPrinter.PrintDeviceInfoModel(result);
        }

        /// <summary>
        /// Method prints compared parameter list.
        /// </summary>
        /// <param name="model"></param>
        public void PrintParametersList(ParameterListModel model)
        {
            var mapper = new ParameterListMapper(SourceData, TargetData);
            var result = mapper.Map(model);
            ModelPrinter.PrintParameterListModel(result);
        }
    }
}
