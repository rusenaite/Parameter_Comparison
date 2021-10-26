using ParameterComparison.src.CLI.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class UIPrinter
    {
        private Dictionary<string, string> SourceData { get; set; }
        private Dictionary<string, string> TargetData { get; set; }

        /// <summary>
        /// Method read data from given source and target paths and assigns
        /// read data to class dictionary collections.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        internal void ReadData(string sourcePath, string targetPath)
        {
            SourceData = FileReader.ReadGZippedFiles(sourcePath);
            TargetData = FileReader.ReadGZippedFiles(targetPath);
        }

        internal int GetActionRequest(InputValidation inputValidator)
        {
            return inputValidator.GetActionChoice(InputValidation.ActionFilter);
        }

        /// <summary>
        /// Method prints main menu with actions for user to choose from.
        /// </summary>
        public static void PrintMainMenu()
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

        public static void PrintRequest(RequestModel model)
        {
            DeviceInfoModel devInfoModel = new DeviceInfoModel();

            if(model.GetType() == devInfoModel.GetType())
            {
                PrintDeviceInfo(model);
            }

            switch (model.GetType())
            {
                case typeof(DeviceInfoModel):
                    PrintDeviceInfo(model);
                    break;

            }
        }

        public static void PrintDeviceInfo(DeviceInfoModel model)
        {

        }
    }
}
