using System.Collections.Generic;
using Confluent.Kafka;
using Google.Protobuf;

namespace Publisher
{
    public class ProtoSerializer<T> : ISerializer<T> where T : Google.Protobuf.IMessage<T>
    {
        public IEnumerable<KeyValuePair<string, object>>
            Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
            => config;

        public byte[] Serialize(T data, SerializationContext context) => data.ToByteArray();
    }
}