using System;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace LabBenchmarks.MessagePack
{
    public class ByteSizeColumn : IColumn
    {
        private readonly Func<Summary, BenchmarkCase, string> _getResult;

        public ByteSizeColumn(Func<Summary, BenchmarkCase, string> getResult) => _getResult = getResult;

        public string Id => nameof(ByteSizeColumn);

        public string ColumnName => "ByteSize";

        public bool AlwaysShow => true;

        public ColumnCategory Category => ColumnCategory.Custom;

        public int PriorityInCategory => 0;

        public bool IsNumeric => true;

        public UnitType UnitType => UnitType.Size;

        public string Legend => $"Custom '{ColumnName}' tag column";

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => _getResult(summary, benchmarkCase);

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, ISummaryStyle style) => GetValue(summary, benchmarkCase);

        public bool IsAvailable(Summary summary) => true;

        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
    }
}
