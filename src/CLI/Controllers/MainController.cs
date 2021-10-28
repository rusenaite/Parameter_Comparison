using ParameterComparison.src.CLI.Mappers;
using ParameterComparison.src.CLI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
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
        internal int GetAction(InputValidation inputValidator)
        {
            return inputValidator.GetActionChoice(InputValidation.ActionFilter);
        }

        /// <summary>
        /// Method calls user input validation for entering input of selected filter.
        /// </summary>
        /// <param name="inputValidator"></param>
        /// <param name="filter"></param>
        /// <returns> Calls method <see cref="InputValidation.GetFilter(string)"> GetFilter(string) </see> </returns>
        internal static string GetFilter(InputValidation inputValidator, string filter)
        {
            return inputValidator.GetFilter(filter);
        }

        /// <summary>
        /// Method returns requested model by passed action parameter.
        /// </summary>
        /// <param name="action"></param>
        /// <returns> If available action parameter passed, returns one of the request models,
        /// otherwise - throws an exception. </returns>
        public IRequestModel GetRequestedModel(int action)
        {
            switch (action)
            {
                case 0:
                    var parameterListModel = new ParameterListModel(SourceData, TargetData);
                    return (IRequestModel)parameterListModel;
                case 1:
                    var resultSummaryModel = new ResultsSummaryModel(SourceData, TargetData);
                    return (IRequestModel)resultSummaryModel;
                case 2:
                    var filteredDataByIdModel = new FilteredDataByIdModel(SourceData, TargetData);
                    return (IRequestModel)filteredDataByIdModel;
                case 3:
                    var filteredDataByComparisonResultModel = new FilteredDataByComparisonResultModel(SourceData, TargetData);
                    return (IRequestModel)filteredDataByComparisonResultModel;
                default:
                    throw new NotImplementedException();
            }
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
                                "\n\nEnter your choice (number from interval [ 0 ; 3 ]:");
        }

        /// <summary>
        /// Method prints comparison result choices for user to choose from.
        /// </summary>
        public static void PrintComparisonResultChoices()
        {
            Console.WriteLine(@"Select action You would like to perform:" +
                                "\n[U] Unchanged" +
                                "\n[A] Added" +
                                "\n[M] Modified" +
                                "\n[R] Removed" +
                                "\n\nEnter your choice (one of 4 capital letters):");
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
                case FilteredDataByIdModel:
                    PrintFilteredByIdList((FilteredDataByIdModel)model);
                    break;
                case FilteredDataByComparisonResultModel:
                    PrintFilteredByComparisonResultList((FilteredDataByComparisonResultModel)model);
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
        private static void PrintResultSummary(ResultsSummaryModel model)
        {
            var mapper = new ResultsSummaryMapper();
            var result = mapper.Map(model);
            mapper.Print(result);
        }

        /// <summary>
        /// Method prints compared parameters list filtered by entered comparison result.
        /// </summary>
        /// <param name="model"></param>
        private static void PrintFilteredByComparisonResultList(FilteredDataByComparisonResultModel model)
        {
            var mapper = new FilteredDataByComparisonResultMapper();

            PrintComparisonResultChoices();
            InputValidation inputValidation = new();
            var choice = GetFilter(inputValidation, InputValidation.LetterFilter);

            var result = mapper.Map(model, choice);
            mapper.Print(result);
        }

        /// <summary>
        /// Method prints compared parameter list filtered by enter ID (or a part of ID value).
        /// </summary>
        /// <param name="model"></param>
        private static void PrintFilteredByIdList(FilteredDataByIdModel model)
        {
            var mapper = new FilteredDataByIdMapper();

            InputValidation inputValidation = new();
            var id = GetFilter(inputValidation, InputValidation.IdFilter);

            var result = mapper.Map(model, id);
            mapper.Print(result);
        }

        /// <summary>
        /// Method prints device configuration information.
        /// </summary>
        /// <param name="model"></param>
        public static void PrintDeviceInfo(DeviceInfoModel model)
        {
            var mapper = new DeviceInfoMapper();

            var sourceResult = mapper.MapSource(model);
            var targetResult = mapper.MapTarget(model);

            mapper.Print(sourceResult, sourcePath);
            mapper.Print(targetResult, targetPath);
        }

        /// <summary>
        /// Method prints compared parameter list.
        /// </summary>
        /// <param name="model"></param>
        public static void PrintParametersList(ParameterListModel model)
        {
            var mapper = new ParameterListMapper();
            var result = mapper.Map(model);
            mapper.Print(ParameterListModel.SourceData, ParameterListModel.TargetData, result);
        }
        
    }
}
