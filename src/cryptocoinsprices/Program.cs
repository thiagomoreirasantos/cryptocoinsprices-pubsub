// See https://aka.ms/new-console-template for more information
using System.Threading.Channels;
using cryptcoinsprices;
using cryptocoinsprices;

const string DIRECTORY_PATH = "/cryptocoinsprices/archive/D1";

if (!Directory.Exists(DIRECTORY_PATH))
{
    throw new DirectoryNotFoundException($"Directory {DIRECTORY_PATH} not found.");
}

string[] files = Directory.GetFiles(DIRECTORY_PATH);

files = files.Where(file => file.EndsWith(".csv")).ToArray();

foreach (var file in files)
{
    Console.WriteLine($"Processing {file}");

    Channel<string> channel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions());
    
    var content = await FileProcessor.ProcessAsync(file ?? "");

    Producer producer = new Producer(channel.Writer);
    Consumer consumer = new Consumer(channel.Reader);

    await producer.ProduceAsync(content);
    await consumer.ConsumeAsync();

    channel.Writer.TryComplete();
}

Console.WriteLine("Finish.");