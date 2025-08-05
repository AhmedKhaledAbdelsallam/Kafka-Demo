using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "127.0.0.1:9092",
    GroupId = "demo-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Null, string>(config).Build();
consumer.Subscribe("demo-topic");

Console.WriteLine("📡 Listening for messages (CTRL+C to quit)...");
var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

try
{
    while (true)
    {
        var cr = consumer.Consume(cts.Token);
        Console.WriteLine($"📥 Received: {cr.Value}");
    }
}
catch (OperationCanceledException)
{
    consumer.Close();
}
