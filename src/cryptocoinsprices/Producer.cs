using System.Threading.Channels;

namespace cryptocoinsprices
{
    public class Producer : IDisposable
    {
        ChannelWriter<string> _writer;
        public Producer(ChannelWriter<string> writer)
        {
            _writer = writer;
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task ProduceAsync(string fileEntry)
        {
            try
            {
                Console.WriteLine("Producer started");

                Console.WriteLine($"Producing {fileEntry}");
                while (await _writer.WaitToWriteAsync().ConfigureAwait(false))
                    if (_writer.TryWrite(fileEntry))
                        break;

                Console.WriteLine("Producer completed");

                _writer.Complete();

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error producing files.{ex.ToString()}");
            }
        }
    }
}