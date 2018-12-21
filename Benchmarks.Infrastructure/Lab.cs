using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace Benchmarks.Infrastructure
{
    [CoreJob]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [RankColumn, MemoryDiagnoser]
    public class Lab
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

        [Benchmark(Baseline = true), BenchmarkCategory("Yield")]
        public int Yield()
        {
            int a = 0;
            foreach (var item in GetForYield(Count))
            {
                if (item == Count / 2)
                {
                    break;
                }
                a += item;
            }
            return a;
        }

        private IEnumerable<int> GetForYield(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Thread.Sleep(1);
                yield return i;
            }
        }

        [Benchmark, BenchmarkCategory("Yield")]
        public int NoYield()
        {
            int a = 0;
            foreach (var item in GetForNoYield(Count))
            {
                if (item == Count / 2)
                {
                    break;
                }
                a += item;
            }
            return a;
        }

        private IEnumerable<int> GetForNoYield(int n)
        {
            int[] result = new int[n];
            for (int i = 0; i < n; i++)
            {
                Thread.Sleep(1);
                result[i] = i;
            }
            return result;
        }
    }
}
