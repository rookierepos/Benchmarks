using System;
using BenchmarkDotNet.Running;

namespace Benchmarks.Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Lab>();
            Console.ReadKey();
        }
    }
}
