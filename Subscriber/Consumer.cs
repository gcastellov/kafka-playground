using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Confluent.Kafka;
using DataContracts;

namespace Subscriber
{
    public class Consumer
    {
        private readonly string _topic;
        private readonly string _consumerId;
        private readonly ConsumerConfig _config;

        public Consumer(string topic, string consumerGroup, string consumerId)
        {
            _topic = topic;
            _consumerId = consumerId;

            _config = new ConsumerConfig(new Dictionary<string, string>
            {
                { "group.id", consumerGroup },
                { "bootstrap.servers", "localhost:9092" },
                { "enable.auto.commit", "false" },
                { "metadata.max.age.ms", "1000" }
            });
        }

        public void StartConsuming()
        {
            var consumerBuilder = new ConsumerBuilder<string, Payload>(_config);
            consumerBuilder.SetValueDeserializer(new ProtoDeserializer<Payload>());

            using (var consumer = consumerBuilder.Build())
            {
                try
                {
                    consumer.Subscribe(_topic);

                    while (true)
                    {
                        var result = consumer.Consume(CancellationToken.None);
                        Console.WriteLine("From key {0} value: {1}", result.Key, result.Value);
                        Save(result.Key, result.Value);
                        consumer.Commit(result);
                    }
                }
                finally
                {
                    consumer.Close();
                }
            }
        }

        private void Save(string key, Payload payload)
        {
            string file = $"output\\{key}-{_consumerId}.txt";
            using TextWriter writer = File.AppendText(file);
            writer.WriteLine(payload.Value);
        }
    }
}