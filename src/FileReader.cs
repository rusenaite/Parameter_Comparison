using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using Salaros.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Data;

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

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string fileText = sr.ReadToEnd();
                    string[] tokens = fileText.Split(';');

                    foreach (string token in tokens)
                    {
                        string[] keyValuePair = token.Split(':');
                        foreach(var item in keyValuePair)
                        {
                            var convertedItem = Convert.ToHexString(Encoding.ASCII.GetBytes(item));

                            data.Add(convertedItem);
                            Console.WriteLine(convertedItem);
                        }
                    }
                }
            }
            return data;
        }
    }
}
