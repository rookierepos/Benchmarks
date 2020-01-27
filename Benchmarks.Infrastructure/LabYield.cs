using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks.Infrastructure
{
    // [SimpleJob(RunStrategy.ColdStart, targetCount : 50)]

    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [RankColumn, MemoryDiagnoser]
    public class LabYield
    {
        [Params(1, 10, 100, 1000)]
        public int Count { get; set; }

        [Benchmark(Baseline = true)]
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

        [Benchmark]
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
