using System;
using System.Collections.Generic;
using System.IO;

namespace ParameterComparison
{
    class Program
    {
        public const string sourcePath = "C:/Users/raust/source/repos/ParameterComparison/data_samples/FMB920-default.cfg";
        public const string targetPath = "C:/Users/raust/source/repos/ParameterComparison/data_samples/FMB920-modified.cfg";
        static void Main()
        {
            Dictionary<string, string> sourceData = FileReader.ReadGZippedFiles(sourcePath);
            Dictionary<string, string> targetData = FileReader.ReadGZippedFiles(targetPath);

            IConfigFilePrinter configPrinter = new ConfigurationComparison();

            configPrinter.ViewDeviceConfigInfo(sourceData, sourcePath);
            configPrinter.ViewDeviceConfigInfo(targetData, targetPath);

            //configPrinter.ViewParameterList(sourceData, targetData);

            //configPrinter.ViewComparisonResultsSummary(sourceData, targetData);

            string filter = "40";
            //configPrinter.ViewFilteredParameters(sourceData, targetData, filter);

            string choice = "M";
            configPrinter.ViewParamByComparisonResult(sourceData, targetData, choice);
        }
    }
}
