using System.Threading.Channels;

namespace cryptocoinsprices
{
    public class Consumer : IDisposable
    {
        ChannelReader<string> _reader;

        public Consumer(ChannelReader<string> reader)
        {
            _reader = reader;
        }

        public async Task ConsumeAsync()
        {
            try
            {
                Console.WriteLine("Consumer started");

                var cts = new CancellationTokenSource();

                while (await _reader.WaitToReadAsync())
                {
                    var fileEntries = _reader.ReadAllAsync(cts.Token);
                    await foreach (var fileEntry in fileEntries)
                    {
                        Console.WriteLine($"Consumer: {fileEntry}");
                    }
                }
                Console.WriteLine("Consumer completed");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error consuming files.{ex.ToString()}");
            }
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}