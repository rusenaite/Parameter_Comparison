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

            Dictionary<string, string> cfgFiles = FileReader.ReadGZippedFiles(path);
        }
    }
}
