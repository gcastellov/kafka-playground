using System;
using Confluent.Kafka;
using Google.Protobuf;

namespace Subscriber
{
    public class ProtoDeserializer<T> : IDeserializer<T>
        where T : Google.Protobuf.IMessage<T>, new()
    {
        private readonly MessageParser<T> _parser;

        public ProtoDeserializer()
        {
            _parser = new MessageParser<T>(() => new T());
        }

        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
            {
                return default(T);
            }

            return _parser.ParseFrom(data.ToArray());
        }
    }
}