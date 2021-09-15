using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using Salaros.Configuration;
using System.IO;

namespace ParameterComparison
{
    public static class FileReader
    {
        /// <summary>
        /// Method extracts zipped files to a new predefined folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns> An extracted files path, if unable to extract - an empty string. </returns>
        public static string ExtractZippedFiles(string path)
        {
            string dataSamplesPath = Path.GetFullPath(Path.Combine(path, @"..\"));
            string extractPath = Path.Combine(dataSamplesPath, @".\extract");
            ZipFile.ExtractToDirectory(path, extractPath);

            return extractPath;
        }

        public static List<dynamic> ReadCfgFile(string path)
        {
            List<dynamic> data = new();

            var cfgFile = new ConfigParser(path);

            cfgFile.GetValue("Numbers", "ConfigurationVersion");
            cfgFile.GetValue("Strings", "FmType");

            return data;
        }
    }
}
