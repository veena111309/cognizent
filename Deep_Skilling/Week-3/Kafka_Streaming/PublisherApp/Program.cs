using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace PublisherApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All // Guarantees consistency across all replicas
            };

            Console.WriteLine("======================================");
            Console.WriteLine("   Enterprise Kafka Message Publisher ");
            Console.WriteLine("======================================\n");
            Console.WriteLine("Connecting to Kafka cluster at 'localhost:9092'...");

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            Console.WriteLine("Connection ready. Type your messages below (Type 'quit' to close).");

            while (true)
            {
                Console.Write("\nPublish Message > ");
                string rawInput = Console.ReadLine() ?? "";

                if (rawInput.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (string.IsNullOrWhiteSpace(rawInput))
                {
                    continue;
                }

                try
                {
                    var deliveryReport = await producer.ProduceAsync(
                        "enterprise-chat-topic",
                        new Message<Null, string> { Value = rawInput }
                    );

                    Console.WriteLine($">> Delivered to Partition: {deliveryReport.Partition} | Offset: {deliveryReport.Offset}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($">> Publish Error: {e.Error.Reason}");
                }
            }

            Console.WriteLine("Publisher shut down successfully.");
        }
    }
}
