using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;

namespace ParameterComparison.src.Models
{
    public class IdFilterModel : IRequestModel
    {
        public List<ComparedParam> FilteredDataById { get; set; }
    }
}
