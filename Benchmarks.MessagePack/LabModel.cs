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
        public byte[][] ValueArray;

        public MsgpackN(LabModel[] original) : base(original.Length)
        {
            ValueArray = new byte[original.Length][];
            for (int i = 0; i < original.Length; i++)
            {
                ValueArray[i] = MessagePackSerializer.Serialize(original[i]);
            }
        }
    }

    public class LZ4MsgpackN : N
    {
        public byte[][] ValueArray;

        public LZ4MsgpackN(LabModel[] original) : base(original.Length)
        {
            ValueArray = new byte[original.Length][];
            for (int i = 0; i < original.Length; i++)
            {
                ValueArray[i] = LZ4MessagePackSerializer.Serialize(original[i]);
            }
        }
    }

    public class ProtoN : N
    {
        public byte[][] ValueArray;

        public ProtoN(LabModel[] original) : base(original.Length)
        {
            ValueArray = new byte[original.Length][];
            using(var ms = new MemoryStream())
            {
                for (int i = 0; i < original.Length; i++)
                {
                    Serializer.Serialize<LabModel>(ms, original[i]);
                    ValueArray[i] = ms.ToArray();
                    ms.Position = 0;
                }
            }
        }
    }

    public class JsonN : N
    {
        public string[] ValueArray;

        public JsonN(LabModel[] original) : base(original.Length)
        {
            ValueArray = new string[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                ValueArray[i] = JsonConvert.SerializeObject(original[i]);
            }
        }
    }

    public class OriginalN : N
    {
        public LabModel[] ValueArray;

        public OriginalN(int size) : base(size)
        {
            ValueArray = new LabModel[size];
            Enumerable.Range(1, size).ToList().ForEach((x) =>
            {
                ValueArray[x - 1] = new LabModel
                {
                Id = x,
                Name = $"My name is {x}. 我的名字是 {x}。",
                CreatedTime = DateTime.Now,
                Male = x % 2 == 0 ? true : false
                };
            });
        }

        public OriginalN(LabModel[] array) : base(array.Length)
        {
            ValueArray = array;
        }
    }
}
