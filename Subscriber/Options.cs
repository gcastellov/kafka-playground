using CommandLine;

namespace Subscriber
{
    public class Options
    {
        [Option('b', "brokers", Required = false, HelpText = "The broker list of endpoints")]
        public string Brokers { get; set; }

        [Option('t', "topic", Required = true, HelpText = "The topic to consume from")]
        public string Topic { get; set; }

        [Option('g', "group", Required = true, HelpText = "The consumer group the consumer belongs to")]
        public string ConsumerGroup { get; set; }

        [Option('o', "output", Required = false, HelpText = "The path where to save the output files")]
        public string OutputPath { get; set; }
    }
}