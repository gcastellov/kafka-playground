using CommandLine;

namespace Subscriber
{
    public class Options
    {
        [Option('t', "topic", Required = true, HelpText = "The topic to consume from")]
        public string Topic { get; set; }

        [Option('g', "group", Required = true, HelpText = "The consumer group the consumer belongs to")]
        public string ConsumerGroup { get; set; }
    }
}