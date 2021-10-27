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
    public class UIPrinter
    {
        private static string sourcePath;
        private static string targetPath;

        public static Dictionary<string, string> SourceData
        {
            get => SourceData;
            set => FileReader.ReadGZippedFiles(sourcePath);
        }

        public static Dictionary<string, string> TargetData
        {
            get => TargetData;
            set => FileReader.ReadGZippedFiles(targetPath);
        }

        /// <summary>
        /// Constructor sets passed source and target paths.
        /// </summary>
        /// <param name="_sourcePath"></param>
        /// <param name="_targetPath"></param>
        public UIPrinter(string _sourcePath, string _targetPath)
        {
            sourcePath = _sourcePath;
            targetPath = _targetPath;
        }

        internal int GetAction(InputValidation inputValidator)
        {
            return inputValidator.GetActionChoice(InputValidation.ActionFilter);
        }

        internal static string GetFilter(InputValidation inputValidator, string filter)
        {
            return inputValidator.GetFilter(filter);
        }

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

        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static void PrintInputError()
        {
            Console.Error.Write("[ Error ]: Unavailable input entered. Re-enter your choice: ");
        }

        public void PrintRequest(IRequestModel model)
        {
            var devInfoModel = new DeviceInfoModel(SourceData, TargetData);
            var parameterListModel = new ParameterListModel(SourceData, TargetData);
            var filteredByComparisonResultModel = new FilteredDataByComparisonResultModel(SourceData, TargetData);
            var filteredByIdModel = new FilteredDataByIdModel(SourceData, TargetData);
            var resultSummaryModel = new ResultsSummaryModel(SourceData, TargetData);

            if (model.GetType() == devInfoModel.GetType())
            {
                PrintDeviceInfo(devInfoModel);
            }
            else if (model.GetType() == parameterListModel.GetType())
            {
                PrintParametersList(parameterListModel);
            }
            else if (model.GetType() == filteredByIdModel.GetType())
            {
                PrintFilteredByIdList(filteredByIdModel);
            }
            else if (model.GetType() == filteredByComparisonResultModel.GetType())
            {
                PrintFilteredByComparisonResultList(filteredByComparisonResultModel);
            }
            else if (model.GetType() == resultSummaryModel.GetType())
            {
                PrintResultSummary(resultSummaryModel);
            }
            else
            {
                throw new NotImplementedException();
            }

            /*
            switch (model.GetType())
            {
                case DeviceInfoModel:
                    PrintDeviceInfo(model);
                    break;
                case typeof(ParameterListModel):
                    PrintParametersList(model);
                    break;
            }
            */

        }

        private static void PrintResultSummary(ResultsSummaryModel model)
        {
            var mapper = new ResultsSummaryMapper();
            var result = mapper.Map(model);
            mapper.Print(result);
        }

        private static void PrintFilteredByComparisonResultList(FilteredDataByComparisonResultModel model)
        {
            var mapper = new FilteredDataByComparisonResultMapper();

            PrintComparisonResultChoices();
            InputValidation inputValidation = new();
            var choice = GetFilter(inputValidation, InputValidation.LetterFilter);

            var result = mapper.Map(model, choice);
            mapper.Print(result);
        }

        private static void PrintFilteredByIdList(FilteredDataByIdModel model)
        {
            var mapper = new FilteredDataByIdMapper();

            InputValidation inputValidation = new();
            var id = GetFilter(inputValidation, InputValidation.LetterFilter);

            var result = mapper.Map(model, id);
            mapper.Print(result);
        }

        public static void PrintDeviceInfo(DeviceInfoModel model)
        {
            var mapper = new DeviceInfoMapper();

            var sourceResult = mapper.MapSource(model);
            var targetResult = mapper.MapTarget(model);

            mapper.Print(sourceResult, sourcePath);
            mapper.Print(targetResult, targetPath);
        }

        public static void PrintParametersList(ParameterListModel model)
        {
            var mapper = new ParameterListMapper();
            var result = mapper.Map(model);
            mapper.Print(ParameterListModel.SourceData, ParameterListModel.TargetData, result);
        }
        
    }
}
