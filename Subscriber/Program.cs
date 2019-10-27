using System;
using CommandLine;

namespace Subscriber
{
    class Program
    {
        public static Guid ConsumerId = Guid.NewGuid();

        static void Main(string[] args)
        {
            Console.WriteLine("Im the subscriber with id: {0}", ConsumerId);

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var settings = Settings.Create(ConsumerId, o.ConsumerGroup, o.Topic, o.Brokers);
                    var consumer = new Consumer(settings);
                    consumer.StartConsuming();
                });
        }
    }
}
