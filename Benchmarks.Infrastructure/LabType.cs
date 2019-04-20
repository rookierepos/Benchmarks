using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks.Infrastructure
{
    // [SimpleJob(RunStrategy.ColdStart, targetCount : 50)]
    [CoreJob]
    [MaxWarmupCount(7), MaxIterationCount(16), IterationTime(500)]
    [RankColumn, MemoryDiagnoser]
    public class LabType
    {
        [Params(10000)]
        public int Count { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _ = typeof(TestClass).GetTableName1();
        }

        [Benchmark(Baseline = true)]
        public void GetTable1()
        {
            for (int i = 0; i < Count; i++)
            {
                _ = typeof(TestClass).GetTableName1();
            }
        }

        [Benchmark]
        public void GetTable2()
        {
            for (int i = 0; i < Count; i++)
            {
                _ = typeof(TestClass).GetTableName2();
            }
        }
    }

    public static class TestExtension
    {
        private static readonly ConcurrentDictionary<string, string> _tableNameDictionary;

        static TestExtension()
        {
            _tableNameDictionary = new ConcurrentDictionary<string, string>();
        }

        public static string GetTableName1(this Type type)
        {
            if (_tableNameDictionary.ContainsKey(type.FullName))
            {
                return _tableNameDictionary[type.FullName];
            }
            else
            {
                var tableName = GetTableName2(type);
                _tableNameDictionary.TryAdd(type.FullName, tableName);
                return tableName;
            }
        }

        public static string GetTableName2(this Type type)
        {
            var tableName = (type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute)?.Name ?? type.Name;
            return tableName;
        }
    }

    [Table("Test")]
    public class TestClass
    {

    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TableAttribute : Attribute
    {
        public string Name { get; private set; }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
