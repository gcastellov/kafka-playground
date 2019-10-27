using System;
using CommandLine;

namespace Publisher
{
    class Program
    {
        private static readonly Guid ProducerId = Guid.NewGuid();

        static void Main(string[] args)
        {
            Console.WriteLine("Im the publisher...");

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var settings = Settings.Create(ProducerId, o.Topic, o.Brokers);
                    var producer = new Producer(settings, o.From, o.To);
                    producer.StartProducing();

                    Console.WriteLine("All producers have finished");
                });
        }
    }
}
