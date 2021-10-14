using System;
using System.Collections.Generic;
using System.IO;

namespace ParameterComparison
{
    class Program
    {
        public const string sourcePath = "C:/Users/raust/source/repos/ParameterComparison/data_samples/FMB001-default.cfg";
        public const string targetPath = "C:/Users/raust/source/repos/ParameterComparison/data_samples/FMB920-modified.cfg";
        static void Main()
        {

            Dictionary<string, string> sourceData = FileReader.ReadGZippedFiles(sourcePath);
            Dictionary<string, string> targetData = FileReader.ReadGZippedFiles(targetPath);


            IUserInput inputValidator = new InputValidation();

            int actionChoice = inputValidator.GetActionChoice();
            string idFilter = inputValidator.GetIdFilter();
            string stateFilter = inputValidator.GetLetterFilter();


        }
    }
}
