using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using DataContracts;

namespace Publisher
{
    public class Producer
    {
        private readonly Guid _producerId;
        private readonly string _topic;
        private readonly int _from;
        private readonly int _to;
        private readonly ProducerConfig _config;

        public Producer(Guid producerId, string topic, int from, int to)
        {
            _producerId = producerId;
            _topic = topic;
            _from = from;
            _to = to;

            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                MessageTimeoutMs = 0
            };
        }

        public void StartProducing()
        {
            var result = Parallel.For(_from, _to, index => StartProducer(index));

            while (!result.IsCompleted)
            {
                Thread.Sleep(300);
            }
        }

        private void StartProducer(double seed)
        {
            Console.WriteLine("Producing {0}", seed);

            var producerBuilder = new ProducerBuilder<string, Payload>(_config);
            producerBuilder.SetValueSerializer(new ProtoSerializer<Payload>());

            using (var producer = producerBuilder.Build())
            {
                for (long i = 0; i < 1000; i++)
                {
                    string payload = $"{seed}-{i}".ToString(CultureInfo.InvariantCulture);
                    string key = "sc-" + seed;

                    var message = new Message<string, Payload>
                    {
                        Key = key,
                        Value = new Payload
                        {
                            Key = key,
                            ProducerId = _producerId.ToString(),
                            Value = payload
                        }
                    };

                    producer.Produce(_topic, message);
                }

                producer.Flush(TimeSpan.FromSeconds(30));
            }

            Console.WriteLine("End of {0}", seed);
        }
    }
}