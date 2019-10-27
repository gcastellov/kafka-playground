using System;
using CommandLine;

namespace Subscriber
{
    class Program
    {
        public static string ConsumerId = Guid.NewGuid().ToString();

        static void Main(string[] args)
        {
            Console.WriteLine("Im the subscriber with id: {0}", ConsumerId);

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var consumer = new Consumer(o.Topic, o.ConsumerGroup, ConsumerId);
                    consumer.StartConsuming();
                });
        }
    }
}
