using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MessagePack;
using Newtonsoft.Json;
using ProtoBuf;
namespace LabBenchmarks.MessagePack
{
    [ProtoContract]
    [MessagePackObject(keyAsPropertyName: true)]
    public class LabModel
    {
        [ProtoMember(1)]
        // [Key(0)]
        public int Id { get; set; }

        [ProtoMember(2)]
        // [Key(1)]
        public string Name { get; set; }

        [ProtoMember(3)]
        // [Key(2)]
        public DateTime CreatedTime { get; set; }

        [ProtoMember(4)]
        // [Key(3)]
        public bool Male { get; set; }
    }

    public static class LabHelper
    {

        // public static int[] N = { 1, 10 };
        public static int[] N = { 1, 10, 100, 10000 };
        public static List<LabModel> Original;
        public static List<byte[]> Msgpack;
        public static List<byte[]> LZ4Msgpack;
        public static List<byte[]> ProtoBuf;
        public static List<string> Json;

        public static void Init()
        {
            int max = N.Max();
            if (Original == null)
            {
                Original = new List<LabModel>();
                Msgpack = new List<byte[]>();
                LZ4Msgpack = new List<byte[]>();
                ProtoBuf = new List<byte[]>();
                Json = new List<string>();
                Enumerable.Range(1, max).ToList().ForEach((i) =>
                {
                    var model = new LabModel { Id = i, Name = $"My name is {i}. 我的名字是 {i}。", CreatedTime = DateTime.Now, Male = i % 2 == 0 ? true : false };
                    Original.Add(model);
                    Msgpack.Add(MessagePackSerializer.Serialize(model));
                    LZ4Msgpack.Add(LZ4MessagePackSerializer.Serialize(model));
                    using(var ms = new MemoryStream())
                    {
                        Serializer.Serialize<LabModel>(ms, model);
                        ProtoBuf.Add(ms.ToArray());
                    }
                    Json.Add(JsonConvert.SerializeObject(model));
                });
            }
        }
    }

    public enum ModelType
    {
        Original,
        Msgpack,
        LZ4Msgpack,
        ProtoBuf,
        Json
    }

    public class N
    {
        public N(int i, ModelType modelType)
        {
            this.Count = i;
            switch (modelType)
            {
                case ModelType.Original:
                    this.Original = LabHelper.Original.Take(i).ToArray();
                    break;
                case ModelType.Msgpack:
                    this.Msgpack = LabHelper.Msgpack.Take(i).ToArray();
                    break;
                case ModelType.LZ4Msgpack:
                    this.LZ4Msgpack = LabHelper.LZ4Msgpack.Take(i).ToArray();
                    break;
                case ModelType.ProtoBuf:
                    this.ProtoBuf = LabHelper.ProtoBuf.Take(i).ToArray();
                    break;
                default:
                    this.Json = LabHelper.Json.Take(i).ToArray();
                    break;
            }
        }

        private int Count;

        public LabModel[] Original;
        public byte[][] Msgpack;
        public byte[][] LZ4Msgpack;
        public byte[][] ProtoBuf;
        public string[] Json;

        public override string ToString() => Count.ToString();
    }
}
