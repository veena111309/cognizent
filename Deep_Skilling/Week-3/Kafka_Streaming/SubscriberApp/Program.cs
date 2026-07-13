using System;
using System.Threading;
using Confluent.Kafka;

namespace SubscriberApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "enterprise-chat-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true // Auto commit offsets to group metadata
            };

            Console.WriteLine("======================================");
            Console.WriteLine("   Enterprise Kafka Message Subscriber");
            Console.WriteLine("======================================\n");
            Console.WriteLine("Connecting to Kafka cluster at 'localhost:9092'...");

            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true; // Prevents process termination
                cts.Cancel();
            };

            try
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe("enterprise-chat-topic");
                Console.WriteLine("Subscription active. Awaiting incoming messages... (Press Ctrl+C to terminate)\n");

                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cts.Token);
                        Console.WriteLine($"[Received] Partition: {consumeResult.Partition} | Offset: {consumeResult.Offset} | Msg: {consumeResult.Message.Value}");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($">> Consume Error: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Triggered when cts.Cancel() is called
            }
            
            Console.WriteLine("\nSubscriber gracefully disconnected.");
        }
    }
}
