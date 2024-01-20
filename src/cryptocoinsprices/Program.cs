// See https://aka.ms/new-console-template for more information
using System.Threading.Channels;
using cryptcoinsprices;
using cryptocoinsprices;

const int CHANNEL_CAPACITY = 1000;

Channel<string> channel = Channel.CreateBounded<string>(CHANNEL_CAPACITY);

var files = await FileProcessor.ProcessAsync();

Producer producer = new Producer(channel.Writer);
Consumer consumer = new Consumer(channel.Reader);

await producer.ProduceAsync(files);
await consumer.ConsumeAsync();

Console.ReadLine();