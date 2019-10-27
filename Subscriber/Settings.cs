using System;
using System.Collections.Generic;
using Confluent.Kafka;

namespace Subscriber
{
    internal class Settings
    {
        private readonly string _consumerGroup;
        private readonly string _brokers;
        
        public Guid ConsumerId { get; }
        public string Topic { get; }

        private Settings(Guid consumerId, string consumerGroup, string topic, string brokers)
        {
            ConsumerId = consumerId;
            _consumerGroup = consumerGroup;
            Topic = topic;
            _brokers = brokers;
        }

        public static Settings Create(Guid consumerId, string consumerGroup, string topic, string brokers)
        {
            return new Settings(consumerId, consumerGroup, topic, brokers ?? "localhost:9092");
        }

        public ConsumerConfig AsConsumerConfig()
        {
            return new ConsumerConfig(new Dictionary<string, string>
            {
                { "group.id", _consumerGroup },
                { "bootstrap.servers", _brokers },
                { "enable.auto.commit", "false" },
                { "metadata.max.age.ms", "1000" }
            });
        }
    }
}