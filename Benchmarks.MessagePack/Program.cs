using System;
using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Running;
using MessagePack;
using Newtonsoft.Json;
using ProtoBuf;

namespace LabBenchmarks.MessagePack
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
