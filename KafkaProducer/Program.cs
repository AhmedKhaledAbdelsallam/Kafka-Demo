using Confluent.Kafka;

var config = new ProducerConfig
{
    BootstrapServers = "127.0.0.1:9092"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

Console.WriteLine("Enter messages to send to Kafka (type 'exit' to quit):");
while (true)
{
    var message = Console.ReadLine();
    if (message?.ToLower() == "exit") break;

    var deliveryResult = await producer.ProduceAsync(
        "demo-topic",
        new Message<Null, string> { Value = message });

    Console.WriteLine($"✅ Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
}
