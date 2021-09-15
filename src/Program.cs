using System;
using System.Collections.Generic;
using System.IO;

namespace ParameterComparison
{
    class Program
    {
        static void Main()
        {
            string path = "C:/Users/raust/source/repos/ParameterComparison/data_samples/data.zip";

            string extractedDataPath = FileReader.ExtractZippedFiles(path);

            string sourceFilePath = Path.Combine(extractedDataPath, @".\extract\FMB920-default.cfg");
            string targetFilePath = Path.Combine(extractedDataPath, @".\extract\FMB920-modified.cfg");

            List <dynamic> sourceCfgFile = FileReader.ReadCfgFile(sourceFilePath);
            List <dynamic> targetCfgFile = FileReader.ReadCfgFile(targetFilePath);

        }
    }
}
