using System;
using System.Collections.Generic;
using System.IO;

namespace ParameterComparison
{
    class Program
    {
        static void Main()
        {
            string sourcePath = "C:/Users/raust/source/repos/ParameterComparison/data_samples/FMB001-default.cfg";
            string targetPath = "C:/Users/raust/source/repos/ParameterComparison/data_samples/FMB920-default.cfg";

            Dictionary<string, string> sourceFile = FileReader.ReadGZippedFiles(sourcePath);
            Dictionary<string, string> targetFile = FileReader.ReadGZippedFiles(targetPath);

            IConfigChangesPrinter configPriter = new ConfigurationComparison();
            configPriter.PrintDeviceConfigInfo(sourceFile, targetFile);
        }
    }
}
