using CommandLine;
using Confluent.Kafka;

namespace Publisher
{
    public class Options
    {
        [Option('b', "brokers", Required = false, HelpText = "The broker list of endpoints")]
        public string Brokers { get; set; }

        [Option('t', "topic", Required = true, HelpText = "The topic to subscribe")]
        public string Topic { get; set; }

        [Option('f', "from", Required = true, HelpText = "The routing key to start from")]
        public int From { get; set; }

        [Option('u', "until", Required = true, HelpText = "The routing key to end with")]
        public int To { get; set; }
    }
}