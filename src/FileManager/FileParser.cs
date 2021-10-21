using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public static class FileParser
    {
        /// <summary>
        /// Method deserializes .CFG format file from given stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns> If parsing went well, returns a dictionary collection of key-value pairs,
        /// otherwise - an empty dictionary collection. </returns>
        public static Dictionary<string, string> ParseCfg(StreamReader stream)
        {
            Dictionary<string, string> keyValuePairs = new();

            string fileText = stream.ReadToEnd();
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
                    }
                }
            }

            return keyValuePairs;
        }
    }
}
