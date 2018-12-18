using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using MessagePack;
using Newtonsoft.Json;
using ProtoBuf;

namespace LabBenchmarks.MessagePack
{
    // [SimpleJob(RunStrategy.ColdStart, targetCount : 5)]
    [CoreJob]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [RankColumn, MemoryDiagnoser]
    [Config(typeof(Config))]
    public class Lab
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(new ByteSizeColumn((benchmarkCase) => GetResultByByte(GetBytesForName(benchmarkCase))));
            }

            byte[] GetBytesForName(BenchmarkCase benchmarkCase)
            {
                string methodName = benchmarkCase.Descriptor.WorkloadMethod.Name;
                if (benchmarkCase.Descriptor.Categories.Contains("Serialize"))
                {
                    int Count = int.Parse(benchmarkCase.Parameters.Items.First(a => a.Name == "Count").Value.ToString());
                    var N = new N(Count);
                    var arr = N.Original;
                    if (methodName.StartsWith("Proto"))
                    {
                        using(var ms = new MemoryStream())
                        {
                            Serializer.Serialize<List<LabModel>>(ms, arr);
                            return ms.ToArray();
                        }
                    }
                    else
                    {
                        if (methodName.StartsWith("Mspack"))
                        {
                            return MessagePackSerializer.Serialize(arr);
                        }
                        else if (methodName.StartsWith("LZ4Mspack"))
                        {
                            return LZ4MessagePackSerializer.Serialize(arr);
                        }
                        else
                        {
                            return Encoding.Default.GetBytes(JsonConvert.SerializeObject(arr));
                        }
                    }
                }
                return null;
            }

            string GetResultByByte(byte[] bytes)
            {
                if (bytes == null || bytes.Length == 0) return "-";
                long len = bytes.Length;
                var a = BenchmarkDotNet.Columns.SizeUnit.GetBestSizeUnit(len);
                return Math.Round(len / (double) a.ByteAmount, 2) + " " + a.Name;
            }
        }

        [Params(1, 10, 100, 10000)]
        public int Count { get; set; }

        private N N;

        [GlobalSetup]
        public void Setup()
        {
            N = new N(Count);
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Serialize")]
        public List<byte[]> MspackSerialize()
        {
            List<byte[]> ret = new List<byte[]>();
            foreach (var item in N.Original)
            {
                ret.Add(MessagePackSerializer.Serialize(item));
            }
            return ret;
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Deserialize")]
        public List<LabModel> MspackDeserialize()
        {
            List<LabModel> ret = new List<LabModel>();
            foreach (var item in N.Msgpack)
            {
                ret.Add(MessagePackSerializer.Deserialize<LabModel>(item));
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        public List<byte[]> LZ4MspackSerialize()
        {
            List<byte[]> ret = new List<byte[]>();
            foreach (var item in N.Original)
            {
                ret.Add(LZ4MessagePackSerializer.Serialize(item));
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        public List<LabModel> LZ4MspackDeserialize()
        {
            List<LabModel> ret = new List<LabModel>();
            foreach (var item in N.LZ4Msgpack)
            {
                ret.Add(LZ4MessagePackSerializer.Deserialize<LabModel>(item));
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        public List<byte[]> ProtoSerialize()
        {
            List<byte[]> ret = new List<byte[]>();
            foreach (var item in N.Original)
            {
                using(var ms = new MemoryStream())
                {
                    Serializer.Serialize<LabModel>(ms, item);
                    ret.Add(ms.ToArray());
                }
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        public List<LabModel> ProtoDeserialize()
        {
            List<LabModel> ret = new List<LabModel>();
            foreach (var item in N.ProtoBuf)
            {
                using(var ms = new MemoryStream(item))
                {
                    ret.Add(Serializer.Deserialize<LabModel>(ms));
                }
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        public List<string> JsonSerialize()
        {
            List<string> ret = new List<string>();
            foreach (var item in N.Original)
            {
                ret.Add(JsonConvert.SerializeObject(item));
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        public List<LabModel> JsonDeserialize()
        {
            List<LabModel> ret = new List<LabModel>();
            foreach (var item in N.Json)
            {
                ret.Add(JsonConvert.DeserializeObject<LabModel>(item));
            }
            return ret;
        }
    }
}
