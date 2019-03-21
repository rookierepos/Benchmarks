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
    [MessagePackObject /*(keyAsPropertyName: true)*/ ]
    public class LabModel
    {
        [ProtoMember(1)]
        [Key(0)]
        public int Id { get; set; }

        [ProtoMember(2)]
        [Key(1)]
        public string Name { get; set; }

        [ProtoMember(3)]
        [Key(2)]
        public DateTime CreatedTime { get; set; }

        [ProtoMember(4)]
        [Key(3)]
        public bool Male { get; set; }

        [ProtoMember(5)]
        [Key(4)]
        public TimeSpan TimeSpan { get; set; }

        [ProtoMember(6)]
        [Key(5)]
        public double Double { get; set; }
    }

    public class N
    {
        public int Size { get; }
        public N(int size)
        {
            Size = size;
        }

        public override string ToString()
        {
            return Size.ToString();
        }
    }

    public class MsgpackN : N
    {
        public byte[] ValueArray;

        public MsgpackN(LabModel[] original) : base(original.Length)
        {
            ValueArray = MessagePackSerializer.Serialize(original);
        }
    }

    public class LZ4MsgpackN : N
    {
        public byte[] ValueArray;

        public LZ4MsgpackN(LabModel[] original) : base(original.Length)
        {
            ValueArray = LZ4MessagePackSerializer.Serialize(original);
        }
    }

    public class ProtoN : N
    {
        public byte[] ValueArray;

        public ProtoN(LabModel[] original) : base(original.Length)
        {
            using(var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, original);
                ValueArray = ms.ToArray();
            }
        }
    }

    public class JsonN : N
    {
        public string ValueArray;

        public JsonN(LabModel[] original) : base(original.Length)
        {
            ValueArray = JsonConvert.SerializeObject(original);
        }
    }

    public class OriginalN : N
    {
        public LabModel[] ValueArray;

        public OriginalN(int size) : base(size)
        {
            ValueArray = new LabModel[size];
            var rand = new Random(DateTime.Now.Millisecond);
            Enumerable.Range(1, size).ToList().ForEach((x) =>
            {
                ValueArray[x - 1] = new LabModel
                {
                Id = x,
                Name = $"My name is {x}. 我的名字是 {x}。",
                CreatedTime = DateTime.Now,
                Male = x % 2 == 0 ? true : false,
                TimeSpan = DateTime.Now - default(DateTime),
                Double = rand.NextDouble()
                };
            });
        }

        public OriginalN(LabModel[] array) : base(array.Length)
        {
            ValueArray = array;
        }
    }
}
