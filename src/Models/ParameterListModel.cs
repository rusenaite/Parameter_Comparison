﻿using ParameterComparison.src.ConfigDataProcessing;
using System.Collections.Generic;

namespace ParameterComparison.src.Models
{
    public class ParameterListModel : IRequestModel
    {
        public List<ComparedParam> ParameterList { get; set; }
    }
}
