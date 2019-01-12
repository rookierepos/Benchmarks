using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Benchmarks.Infrastructure
{
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

        [Benchmark(Baseline = true)]
        public int ListForeach()
        {
            int a = 0;
            foreach (var item in list)
            {
                a += item;
            }
            return a;
        }

        [Benchmark]
        public int IListForeach()
        {
            int a = 0;
            foreach (var item in ilist)
            {
                a += item;
            }
            return a;
        }

        [Benchmark]
        public void AsSpan()
        {
            var result = CreateSpan(Count);
        }

        private Span<string> CreateSpan(int n)
        {
            return Enumerable.Range(1, n).Select(a => $"我是{a}").ToArray().AsSpan();
        }

        [Benchmark]
        public void ToArray()
        {
            var result = CreateArray(Count);
        }

        private string[] CreateArray(int n)
        {
            return Enumerable.Range(1, n).Select(a => $"我是{a}").ToArray();
        }

        [Benchmark]
        public void ToList()
        {
            var result = CreateList(Count);
        }

        private List<string> CreateList(int n)
        {
            return Enumerable.Range(1, n).Select(a => $"我是{a}").ToList();
        }
    }
}
