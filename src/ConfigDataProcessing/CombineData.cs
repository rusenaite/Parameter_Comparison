using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class CombineData
    {
        const string DefaultAction = " ";

        /// <summary>
        /// Method combines source and target data dictionaries of integer type keys.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetData"></param>
        /// <returns> If merging went well, returns a combined data list, otherwise - 
        /// an empty list. </returns>
        public static List<ComparedParam> CombineDictionaries(Dictionary<string, string> sourceData, Dictionary<string, string> targetData)
        {
            List<ComparedParam> data = new();

            foreach (var srcPair in sourceData.GetIntTypeKeys())
            {
                foreach (var trgPair in targetData.GetIntTypeKeys())
                {
                    if (srcPair.Key > trgPair.Key)
                    {
                        continue;
                    }

                    ComparedParam pair = ConfigurationComparison.SetComparedPair(srcPair, trgPair, DefaultAction);
                    data.Add(pair);
                    break;
                }
            }

            return data;
        }

    }
}
