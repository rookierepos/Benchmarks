using System;
using System.Linq;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace LabBenchmarks.MessagePack
{
    public class ByteSizeColumn : IColumn
    {
        public ByteSizeColumn() { }

        public string Id => nameof(ByteSizeColumn);

        public string ColumnName => "ByteSize";

        public bool AlwaysShow => true;

        public ColumnCategory Category => ColumnCategory.Custom;

        public int PriorityInCategory => 0;

        public bool IsNumeric => true;

        public UnitType UnitType => UnitType.Size;

        public string Legend => $"Custom '{ColumnName}' tag column";

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => GetValue(summary, benchmarkCase, null);

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
        {
            var method = benchmarkCase.Descriptor.WorkloadMethod;
            var instance = Activator.CreateInstance(method.DeclaringType);
            var argument = benchmarkCase.Parameters.GetArgument("N").Value;
            var result = method.Invoke(instance, new [] { argument });
            if (method.Name.Contains("Serialize"))
            {
                switch (result)
                {
                    case byte[] b:
                        return ConvertToDisplay(b.LongLength);
                    case string s:
                        return ConvertToDisplay(System.Text.Encoding.UTF8.GetBytes(s).LongLength);
                    default:
                        return "-";
                }
            }
            else
            {
                switch (result)
                {
                    case LabModel[] res:
                        return $"{res?.Length ?? 0}N";
                    default:
                        return "-";
                }
            }
        }

        static string ConvertToDisplay(long size)
        {
            var a = BenchmarkDotNet.Columns.SizeUnit.GetBestSizeUnit(size);
            return Math.Round(size / (double) a.ByteAmount, 2) + " " + a.Name;
        }

        public bool IsAvailable(Summary summary) => true;

        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
    }
}
