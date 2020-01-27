using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks.Infrastructure
{

    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MaxWarmupCount(7), MaxIterationCount(16), IterationTime(500)]
    [RankColumn, MemoryDiagnoser]
    public class LabTask
    {
        [Params(1, 10000)]
        public int Loop { get; set; }

        [Benchmark(Baseline = true)]
        public void Add()
        {
            int Add(int a, int b)
            {
                return a + b;
            }
            for (int i = 0; i < Loop; i++)
            {
                _ = Add(i, i);
            }
        }

        [Benchmark]
        public Task TaskAdd()
        {
            Task<int> AddAsync(int a, int b)
            {
                return Task.FromResult<int>(a + b);
            }
            for (int i = 0; i < Loop; i++)
            {
                _ = AddAsync(i, i);
            }
            return Task.CompletedTask;
        }

        [Benchmark]
        public Task ValueTaskAdd()
        {
            ValueTask<int> AddAsync(int a, int b)
            {
                return new ValueTask<int>(a + b);
            }
            for (int i = 0; i < Loop; i++)
            {
                _ = AddAsync(i, i);
            }
            return Task.CompletedTask;
        }

    }
}
