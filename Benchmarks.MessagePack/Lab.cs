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
    [CoreJob]
    // [MeanColumn]
    [MaxWarmupCount(7), MaxIterationCount(16), IterationTime(500)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    // [RankColumn]
    [MemoryDiagnoser]
    [Config(typeof(Config))]
    public class Lab
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(new ByteSizeColumn());
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
        public byte[] MspackSerialize(OriginalN N)
        {
            return MessagePackSerializer.Serialize(N.ValueArray);
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[] MspackContractlessSerialize(OriginalN N)
        {
            return MessagePackSerializer.Serialize(N.ValueArray, ContractlessStandardResolver.Instance);
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[] LZ4MspackSerialize(OriginalN N)
        {
            return LZ4MessagePackSerializer.Serialize(N.ValueArray);
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[] LZ4MspackContractlessSerialize(OriginalN N)
        {
            return LZ4MessagePackSerializer.Serialize(N.ValueArray, ContractlessStandardResolver.Instance);
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public byte[] ProtoSerialize(OriginalN N)
        {
            using(var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, N.ValueArray);
                return ms.ToArray();
            }
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public string NewtonJsonSerialize(OriginalN N)
        {
            return JsonConvert.SerializeObject(N.ValueArray);
        }

        [Benchmark, BenchmarkCategory("Serialize")]
        [ArgumentsSource(nameof(OriginalNParam))]
        public string SwifterJsonSerialize(OriginalN N)
        {
            return JsonFormatter.SerializeObject(N.ValueArray);
        }

        [Benchmark(Baseline = true), BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(MsgpackNParam))]
        public LabModel[] MspackDeserialize(MsgpackN N)
        {
            return MessagePackSerializer.Deserialize<LabModel[]>(N.ValueArray);
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(MsgpackNParam))]
        public LabModel[] MspackContractlessDeserialize(MsgpackN N)
        {
            return MessagePackSerializer.Deserialize<LabModel[]>(N.ValueArray, ContractlessStandardResolver.Instance);
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(LZ4MsgpackNParam))]
        public LabModel[] LZ4MspackDeserialize(LZ4MsgpackN N)
        {
            return LZ4MessagePackSerializer.Deserialize<LabModel[]>(N.ValueArray);
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(LZ4MsgpackNParam))]
        public LabModel[] LZ4MspackContractlessDeserialize(LZ4MsgpackN N)
        {
            return LZ4MessagePackSerializer.Deserialize<LabModel[]>(N.ValueArray, ContractlessStandardResolver.Instance);
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(ProtoNParam))]
        public LabModel[] ProtoDeserialize(ProtoN N)
        {
            using(var ms = new MemoryStream(N.ValueArray))
            {
                return Serializer.Deserialize<LabModel[]>(ms);
            }
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(JsonNParam))]
        public LabModel[] NewtonJsonDeserialize(JsonN N)
        {
            return JsonConvert.DeserializeObject<LabModel[]>(N.ValueArray);
        }

        [Benchmark, BenchmarkCategory("Deserialize")]
        [ArgumentsSource(nameof(JsonNParam))]
        public LabModel[] SwifterJsonDeserialize(JsonN N)
        {
            return JsonFormatter.DeserializeObject<LabModel[]>(N.ValueArray);
        }
    }
}
