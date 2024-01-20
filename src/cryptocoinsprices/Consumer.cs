using System.Threading.Channels;

namespace cryptocoinsprices
{
    public class Consumer
    {
        ChannelReader<string> _reader;
        public Consumer(ChannelReader<string> reader)
        {
            _reader = reader;
        }

        public async Task ConsumeAsync()
        {
            while (await _reader.WaitToReadAsync())
            {
                while (_reader.TryRead(out var fileEntry))
                {
                    System.Console.WriteLine(fileEntry);
                }
            }

            Console.WriteLine("Consumer completed");
        }
    }
}