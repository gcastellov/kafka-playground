using System;
using System.IO;
using System.Threading;
using Confluent.Kafka;
using DataContracts;

namespace Subscriber
{
    internal class Consumer
    {
        private readonly Settings _settings;
        private readonly ConsumerConfig _config;

        public Consumer(Settings settings)
        {
            _settings = settings;
            _config = _settings.AsConsumerConfig();
        }

        public void StartConsuming()
        {
            var consumerBuilder = new ConsumerBuilder<string, Payload>(_config);
            consumerBuilder.SetValueDeserializer(new ProtoDeserializer<Payload>());

            using (var consumer = consumerBuilder.Build())
            {
                try
                {
                    consumer.Subscribe(_settings.Topic);

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
            string file = $"{_settings.OutputPath}\\{key}-{_settings.ConsumerId}.txt";
            using TextWriter writer = File.AppendText(file);
            writer.WriteLine(payload.Value);
        }
    }
}