using System;
using System.Linq;
using BenchmarkDotNet.Running;

namespace Benchmarks.Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                if (int.TryParse(args[0].AsSpan(), out int values))
                {
                    if (values >= 0 && values < labs.Length)
                    {
                        _ = BenchmarkRunner.Run(labs[values]);
                        return;
                    }
                }
            }
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
                    _ = BenchmarkRunner.Run(labs[index]);
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
