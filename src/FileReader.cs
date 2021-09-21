using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;

namespace ParameterComparison
{
    public static class FileReader
    {
        /// <summary>
        /// Opens and reads GZip format files.
        /// </summary>
        /// <param name="path"></param>
        /// <returns> If reading went well, dictionary collection type of key-value pairs,
        /// otherwise - an empty dictionary collection. </returns>
        public static Dictionary<string, string> ReadGZippedFiles(string path)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                using GZipStream gzip = new GZipStream(fs, CompressionMode.Decompress);
                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.ASCII))
                {
                    string fileText = sr.ReadToEnd();
                    string[] lines = fileText.Split(';');

                    foreach (string line in lines)
                    {
                        int lineSplit = line.IndexOf(":");

                        if (lineSplit >= 0)
                        {
                            string key = line.Substring(0, lineSplit);
                            string value = line.Substring(lineSplit + 1);

                            if (!keyValuePairs.ContainsKey(key))
                            {
                                keyValuePairs.Add(key, value);
                                Console.WriteLine("Key : {0}, Value : {1}", key, value);
                            }
                        }
                    }
                }
            }

            return keyValuePairs;
        }
    }
}
