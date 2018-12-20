using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace Benchmarks.Interface
{
    [CoreJob]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [RankColumn, MemoryDiagnoser]
    public class LabIEnumerable
    {
        [Params(1, 10, 100, 10000)]
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
    }
}
