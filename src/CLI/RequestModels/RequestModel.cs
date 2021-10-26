using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison.src.CLI.RequestModels
{
    public abstract class RequestModel
    {
        public abstract List<ComparedParam> Viewer();

    }
}
