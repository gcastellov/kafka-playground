using System;
using Confluent.Kafka;

namespace Publisher
{
    internal class Settings
    {
        private readonly string _brokers;
        
        public Guid ProducerId { get; }
        public string Topic { get; }

        private Settings(Guid producerId, string topic, string brokers)
        {
            ProducerId = producerId;
            Topic = topic;
            _brokers = brokers;
        }

        public static Settings Create(Guid producerId, string topic, string brokers)
        {
            return new Settings(producerId, topic, brokers ?? "localhost:9092");
        }

        public ProducerConfig AsProducerConfig()
        {
            return new ProducerConfig
            {
                BootstrapServers = _brokers,
                MessageTimeoutMs = 0
            };
        }
    }
}