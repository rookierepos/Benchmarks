using System;
using BenchmarkDotNet.Running;

namespace Benchmarks.Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LabIEnumerable>();
            Console.ReadKey();
        }
    }
}
