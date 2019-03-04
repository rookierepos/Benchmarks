using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using MessagePack;
using MessagePack.Resolvers;
using Newtonsoft.Json;
using ProtoBuf;
using Swifter.Json;

namespace LabBenchmarks.MessagePack
{
    // [SimpleJob(RunStrategy.Throughput,
    //     launchCount : 1,
    //     warmupCount : 5,
    //     targetCount : 15,
    //     invocationCount: -1)]
    [CoreJob, MaxWarmupCount(7), MaxIterationCount(16), IterationTime(300)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [RankColumn, MemoryDiagnoser]
    [Config(typeof(Config))]
    public class Lab
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(new ByteSizeColumn((summary, benchmarkCase) => GetResultByByte(GetBytesForName(summary, benchmarkCase))));
            }

            byte[] GetBytesForName(Summary summary, BenchmarkCase benchmarkCase)
            {
                if (benchmarkCase.Descriptor.Categories.Contains("Serialize"))
                {
                    OriginalN value = benchmarkCase.Parameters.GetArgument("N").Value as OriginalN;
                    var arr = value.ValueArray;
                    string methodName = benchmarkCase.Descriptor.WorkloadMethod.Name;
                    if (methodName.StartsWith("Proto"))
                    {
                        using(var ms = new MemoryStream())
                        {
                            Serializer.Serialize(ms, arr);
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
                        else if (methodName.StartsWith("SwifterJson"))
                        {
                            return Encoding.Default.GetBytes(JsonFormatter.SerializeObject(arr));
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

        private static int[] Params = { 1, 100, 10000 };

        public OriginalN originalN = new OriginalN(Params.Max());

        private LabModel[] Take(int size)
        {
            return originalN.ValueArray.Take(size).ToArray();
        }

        public IEnumerable<OriginalN> OriginalNParam()
        {
            foreach (var param in Params)
            {
                yield return new OriginalN(Take(param));
            }
        }

        public IEnumerable<MsgpackN> MsgpackNParam()
        {
            foreach (var param in Params)
            {
                yield return new MsgpackN(Take(param));
            }
        }

        public IEnumerable<LZ4MsgpackN> LZ4MsgpackNParam()
        {
            foreach (var param in Params)
            {
                yield return new LZ4MsgpackN(Take(param));
            }
        }

        public IEnumerable<ProtoN> ProtoNParam()
        {
            foreach (var param in Params)
            {
                yield return new ProtoN(Take(param));
            }
        }

        public IEnumerable<JsonN> JsonNParam()
        {
            foreach (var param in Params)
            {
                yield return new JsonN(Take(param));
            }
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[][] MspackSerialize(OriginalN N)
        {
            byte[][] ret = new byte[N.Size][];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = MessagePackSerializer.Serialize(N.ValueArray[i]);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[][] MspackContractlessSerialize(OriginalN N)
        {
            byte[][] ret = new byte[N.Size][];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = MessagePackSerializer.Serialize(N.ValueArray[i], ContractlessStandardResolver.Instance);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[][] LZ4MspackSerialize(OriginalN N)
        {
            byte[][] ret = new byte[N.Size][];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = LZ4MessagePackSerializer.Serialize(N.ValueArray[i]);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[][] LZ4MspackContractlessSerialize(OriginalN N)
        {
            byte[][] ret = new byte[N.Size][];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = LZ4MessagePackSerializer.Serialize(N.ValueArray[i], ContractlessStandardResolver.Instance);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[][] ProtoSerialize(OriginalN N)
        {
            byte[][] ret = new byte[N.Size][];
            using(var ms = new MemoryStream())
            {
                for (int i = 0; i < N.Size; i++)
                {
                    Serializer.Serialize(ms, N.ValueArray[i]);
                    ret[i] = ms.ToArray();
                    ms.Position = 0;
                }
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public string[] NewtonJsonSerialize(OriginalN N)
        {
            string[] ret = new string[N.Size];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = JsonConvert.SerializeObject(N.ValueArray[i]);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public string[] SwifterJsonSerialize(OriginalN N)
        {
            string[] ret = new string[N.Size];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = JsonFormatter.SerializeObject(N.ValueArray[i]);
            }
            return ret;
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(MsgpackNParam))]
        public LabModel[] MspackDeserialize(MsgpackN N)
        {
            LabModel[] ret = new LabModel[N.Size];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = MessagePackSerializer.Deserialize<LabModel>(N.ValueArray[i]);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(MsgpackNParam))]
        public LabModel[] MspackContractlessDeserialize(MsgpackN N)
        {
            LabModel[] ret = new LabModel[N.Size];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = MessagePackSerializer.Deserialize<LabModel>(N.ValueArray[i], ContractlessStandardResolver.Instance);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(LZ4MsgpackNParam))]
        public LabModel[] LZ4MspackDeserialize(LZ4MsgpackN N)
        {
            LabModel[] ret = new LabModel[N.Size];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = LZ4MessagePackSerializer.Deserialize<LabModel>(N.ValueArray[i]);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(LZ4MsgpackNParam))]
        public LabModel[] LZ4MspackContractlessDeserialize(LZ4MsgpackN N)
        {
            LabModel[] ret = new LabModel[N.Size];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = LZ4MessagePackSerializer.Deserialize<LabModel>(N.ValueArray[i], ContractlessStandardResolver.Instance);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(ProtoNParam))]
        public LabModel[] ProtoDeserialize(ProtoN N)
        {
            LabModel[] ret = new LabModel[N.Size];
            using(var ms = new MemoryStream())
            {
                for (int i = 0; i < N.Size; i++)
                {
                    ms.Read(N.ValueArray[i]);
                    ret[i] = Serializer.Deserialize<LabModel>(ms);
                    ms.Flush();
                }
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(JsonNParam))]
        public LabModel[] NewtonJsonDeserialize(JsonN N)
        {
            LabModel[] ret = new LabModel[N.Size];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = JsonConvert.DeserializeObject<LabModel>(N.ValueArray[i]);
            }
            return ret;
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(JsonNParam))]
        public LabModel[] SwifterJsonDeserialize(JsonN N)
        {
            LabModel[] ret = new LabModel[N.Size];
            for (int i = 0; i < N.Size; i++)
            {
                ret[i] = JsonFormatter.DeserializeObject<LabModel>(N.ValueArray[i]);
            }
            return ret;
        }
    }
}
