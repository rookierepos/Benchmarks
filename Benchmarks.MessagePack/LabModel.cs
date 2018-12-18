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

    public class N
    {
        public N(int i)
        {
            Original = new List<LabModel>();
            Msgpack = new List<byte[]>();
            LZ4Msgpack = new List<byte[]>();
            ProtoBuf = new List<byte[]>();
            Json = new List<string>();
            Enumerable.Range(1, i).ToList().ForEach((x) =>
            {
                var model = new LabModel
                {
                Id = x,
                Name = $"My name is {x}. 我的名字是 {x}。",
                CreatedTime = DateTime.Now,
                Male = x % 2 == 0 ? true : false
                };
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

        public List<LabModel> Original;
        public List<byte[]> Msgpack;
        public List<byte[]> LZ4Msgpack;
        public List<byte[]> ProtoBuf;
        public List<string> Json;
    }
}
