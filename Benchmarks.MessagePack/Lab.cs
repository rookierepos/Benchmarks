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
        public Lab()
        {
            LabHelper.Init();
        }

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
                    var N = benchmarkCase.Parameters.GetArgument("N").Value as N;
                    var arr = N.Original;
                    if (methodName.StartsWith("Proto"))
                    {
                        using(var ms = new MemoryStream())
                        {
                            Serializer.Serialize<LabModel[]>(ms, arr);
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

        public IEnumerable<N> GetModel()
        {
            foreach (var i in LabHelper.N)
            {
                yield return new N(i, ModelType.Original);
            }
        }

        public IEnumerable<N> GetMsgpack()
        {
            foreach (var i in LabHelper.N)
            {
                yield return new N(i, ModelType.Msgpack);
            }
        }

        public IEnumerable<N> GetLZ4Msgpack()
        {
            foreach (var i in LabHelper.N)
            {
                yield return new N(i, ModelType.LZ4Msgpack);
            }
        }

        public IEnumerable<N> GetJson()
        {
            foreach (var i in LabHelper.N)
            {
                yield return new N(i, ModelType.Json);
            }
        }

        public IEnumerable<N> GetProto()
        {
            foreach (var i in LabHelper.N)
            {
                yield return new N(i, ModelType.ProtoBuf);
            }
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(GetModel))]
        public List<byte[]> MspackSerialize(N N)
        {
            List<byte[]> ret = new List<byte[]>();
            foreach (var item in N.Original)
            {
                ret.Add(MessagePackSerializer.Serialize(item));
            }
            return ret;
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(GetMsgpack))]
        public List<LabModel> MspackDeserialize(N N)
        {
            List<LabModel> ret = new List<LabModel>();
            foreach (var item in N.Msgpack)
            {
                ret.Add(MessagePackSerializer.Deserialize<LabModel>(item));
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(GetModel))]
        public List<byte[]> LZ4MspackSerialize(N N)
        {
            List<byte[]> ret = new List<byte[]>();
            foreach (var item in N.Original)
            {
                ret.Add(LZ4MessagePackSerializer.Serialize(item));
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(GetLZ4Msgpack))]
        public List<LabModel> LZ4MspackDeserialize(N N)
        {
            List<LabModel> ret = new List<LabModel>();
            foreach (var item in N.LZ4Msgpack)
            {
                ret.Add(LZ4MessagePackSerializer.Deserialize<LabModel>(item));
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(GetModel))]
        public List<byte[]> ProtoSerialize(N N)
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
        [ArgumentsSource(nameof(GetProto))]
        public List<LabModel> ProtoDeserialize(N N)
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
        [ArgumentsSource(nameof(GetModel))]
        public List<string> JsonSerialize(N N)
        {
            List<string> ret = new List<string>();
            foreach (var item in N.Original)
            {
                ret.Add(JsonConvert.SerializeObject(item));
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(GetJson))]
        public List<LabModel> JsonDeserialize(N N)
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
