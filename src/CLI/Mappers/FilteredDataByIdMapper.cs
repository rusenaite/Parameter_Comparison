using ParameterComparison.src.CLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.Mappers
{
    public class FilteredDataByIdMapper
    {
        public List<ComparedParam> Map(FilteredDataByIdModel model, string id)
        {
            return model.Create(id);
        }

        public void Print(List<ComparedParam> result)
        {
            Printers.PrintComparedData(result);
        }
    }
}
