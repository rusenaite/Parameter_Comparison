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
                    Action = ComparisonResult.Unchanged;
                }
                else
                {
                    Action = ComparisonResult.Modified;
                }
            }
            else if (!source.Equals(default(KeyValuePair<string, string>)) && target.Equals(default(KeyValuePair<string, string>)))
            {
                Action = ComparisonResult.Removed;
            }

            SourcePair = source;
            TargetPair = target;
        }

        public KeyValuePair<string, string> SourcePair { get; set; }
        public KeyValuePair<string, string> TargetPair { get; set; }
        public ComparisonResult Action { get; set; }

        public override string ToString()
        {
            if (Action == ComparisonResult.Removed)
            {
                return Action.ToString().PadRight(ColumnWidth / 2) + SourcePair.Key.ToString().PadRight(ColumnWidth / 2) +
                       SourcePair.Value.PadRight(ColumnWidth);
            }
            else if (Action == ComparisonResult.Added)
            {
                return Action.ToString().PadRight(ColumnWidth / 2) + TargetPair.Key.ToString().PadRight(ColumnWidth / 2) +
                       "".PadRight(ColumnWidth) + TargetPair.Value.PadRight(ColumnWidth);
            }

            return Action.ToString().PadRight(ColumnWidth / 2) + SourcePair.Key.ToString().PadRight(ColumnWidth / 2) +
                   SourcePair.Value.PadRight(ColumnWidth) + TargetPair.Value.PadRight(ColumnWidth);
        }
    }

    public enum ComparisonResult
    {
        Unchanged,
        Added,
        Removed,
        Modified
    }
}
