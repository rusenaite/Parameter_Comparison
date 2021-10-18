﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    interface IUserInput
    {
        int GetActionChoice(string regex);
        string GetFilter(string regex);
    }
}
