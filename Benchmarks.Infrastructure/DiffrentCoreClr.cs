using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

namespace Benchmarks.Infrastructure
{
    public class MultipleRuntimes : ManualConfig
    {
        public MultipleRuntimes()
        {
            Add(Job.Default.With(
                CsProjCoreToolchain.From(
                    new NetCoreAppSettings(
                        targetFrameworkMoniker: "netcoreapp2.1",
                        runtimeFrameworkVersion: "2.1.6",
                        name: ".NET Core 2.1"))));
            Add(Job.Default.With(
                CsProjCoreToolchain.From(
                    new NetCoreAppSettings(
                        targetFrameworkMoniker: "netcoreapp2.2",
                        runtimeFrameworkVersion: "2.2.0",
                        name: ".NET Core 2.2"))));
        }
    }

    [Config(typeof(MultipleRuntimes))]
    [RankColumn, MemoryDiagnoser]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class DiffrentCoreClr
    {
        [Params(1000)]
        public int Count { get; set; }

        List<int> list;
        IList<int> ilist;

        [GlobalSetup]
        public void Setup()
        {
            list = new List<int>();
            for (int i = 0; i < Count; i++)
            {
                list.Add(i);
            }
            ilist = list;
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Interface")]
        public int IList()
        {
            int a = 0;
            foreach (var item in ilist)
            {
                a += item;
            }
            return a;
        }

        [Benchmark, BenchmarkCategory("Interface")]
        public int List()
        {
            int a = 0;
            foreach (var item in list)
            {
                a += item;
            }
            return a;
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Span")]
        public void LabSpan()
        {
            var result = CreateSpan(Count);
        }

        private Span<string> CreateSpan(int n)
        {
            return Enumerable.Range(1, n).Select(a => $"我是{a}").ToArray().AsSpan();
        }

        [Benchmark, BenchmarkCategory("Span")]
        public void LabArray()
        {
            var result = CreateArray(Count);
        }

        private string[] CreateArray(int n)
        {
            return Enumerable.Range(1, n).Select(a => $"我是{a}").ToArray();
        }

        [Benchmark, BenchmarkCategory("Span")]
        public void LabList()
        {
            var result = CreateList(Count);
        }

        private List<string> CreateList(int n)
        {
            return Enumerable.Range(1, n).Select(a => $"我是{a}").ToList();
        }
    }
}
