using System;
using System.Linq;
using BenchmarkDotNet.Running;

namespace Benchmarks.Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            string param = string.Empty;
            do
            {
                for (var i = 0; i < labs.Length; i++)
                {
                    System.Console.WriteLine($"[{i}]:{labs[i].Name}");
                }
                System.Console.WriteLine("[q]:quit");
                param = Console.ReadLine();
                ToBenchmarks(param);
            } while (!param.Equals("q", StringComparison.OrdinalIgnoreCase));

        }

        private static void ToBenchmarks(string param)
        {
            if (int.TryParse(param, out int index))
            {
                if (index >= 0 && index < labs.Length)
                {
                    var summary = BenchmarkRunner.Run(labs[index]);
                }
            }
            else if (!param.Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                System.Console.WriteLine("error param.");
            }
        }

        private static Type[] labs = {
            typeof(LabInterface),
            typeof(LabYield),
            typeof(DiffrentCoreClr)
        };
    }
}
