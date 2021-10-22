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
        /// <returns> If parsing went well, returns a dictionary collection of key-value pairs,
        /// otherwise - an empty dictionary collection. </returns>
        public static Dictionary<string, string> ReadGZippedFiles(string path)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            using (FileStream fs = File.Open(path, FileMode.Open))
            using (GZipStream gzip = new GZipStream(fs, CompressionMode.Decompress))
            using (StreamReader sr = new StreamReader(gzip, System.Text.Encoding.ASCII))
            return FileParser.ParseCfg(sr);
        }
    }
}
