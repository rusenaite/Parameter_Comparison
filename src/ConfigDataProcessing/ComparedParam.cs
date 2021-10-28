using System.Collections.Generic;

namespace ParameterComparison.src.ConfigDataProcessing
{
    public class ComparedParam
    {
        private readonly int ColumnWidth = 40;

        public ComparedParam(KeyValuePair<string, string> source, KeyValuePair<string, string> target)
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
            else if (!source.Equals(default(KeyValuePair<string, string>)) && target.Equals(default(KeyValuePair<string, string>)))
            {
                Action = ParamAction.R;
            }

            SourcePair = source;
            TargetPair = target;
        }

        public KeyValuePair<string, string> SourcePair { get; set; }
        public KeyValuePair<string, string> TargetPair { get; set; }
        public ParamAction Action { get; set; }

        public override string ToString()
        {
            if (Action == ParamAction.R)
            {
                return Action.ToString().PadRight(ColumnWidth / 2) + SourcePair.Key.ToString().PadRight(ColumnWidth / 2) +
                       SourcePair.Value.PadRight(ColumnWidth);
            }
            else if (Action == ParamAction.A)
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
