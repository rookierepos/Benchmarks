using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks.Infrastructure
{
    // [SimpleJob(RunStrategy.ColdStart, targetCount : 50)]
    [CoreJob]
    [RankColumn, MemoryDiagnoser]
    public class LabInterface
    {
        [Params(1, 10, 100, 1000)]
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
        public int IList()
        {
            int a = 0;
            foreach (var item in ilist)
            {
                a += item;
            }
            return a;
        }

        [Benchmark]
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
