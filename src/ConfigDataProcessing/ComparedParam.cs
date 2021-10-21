using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterComparison
{
    public class ComparedParam
    {
        private readonly int ColumnWidth = 40;

        public ComparedParam(KeyValuePair<int, string> source, KeyValuePair<int, string> target)
        {
            if (source.Key == target.Key)
            {
                if (source.Value == target.Value)
                {
                    Action = ParamAction.U;
                }
                else
                {
                    Action = ParamAction.M;
                }
            }
            else if (!source.Equals(default(KeyValuePair<int, string>)) && target.Equals(default(KeyValuePair<int, string>)))
            {
                Action = ParamAction.R;
            }

            SourcePair = source;
            TargetPair = target;
        }

        public KeyValuePair<int, string> SourcePair { get; set; }
        public KeyValuePair<int, string> TargetPair { get; set; }
        public ParamAction Action { get; set; }

        public override string ToString()
        {
            if (!SourcePair.Key.Equals(default) && TargetPair.Key.Equals(default))
            {
                return Action.ToString().PadRight(ColumnWidth / 2) + SourcePair.Key.ToString().PadRight(ColumnWidth / 2) +
                       SourcePair.Value.PadRight(ColumnWidth);
            }
            else if(!TargetPair.Key.Equals(default) && SourcePair.Key.Equals(default))
            {
                return Action.ToString().PadRight(ColumnWidth / 2) + TargetPair.Key.ToString().PadRight(ColumnWidth / 2) +
                       "".PadRight(ColumnWidth) + TargetPair.Value.PadRight(ColumnWidth);
            }

            return Action.ToString().PadRight(ColumnWidth / 2) + SourcePair.Key.ToString().PadRight(ColumnWidth / 2) +
                   SourcePair.Value.PadRight(ColumnWidth) + TargetPair.Value.PadRight(ColumnWidth);
        }
    }

    public enum ParamAction
    {
        U,
        A,
        R,
        M
    }
}
