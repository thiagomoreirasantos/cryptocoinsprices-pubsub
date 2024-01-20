using System.Threading.Channels;

namespace cryptocoinsprices
{
    public class Producer
    {
        private ChannelWriter<string> _writer;
        public Producer(ChannelWriter<string> writer)
        {
            _writer = writer;
        }
        public async Task ProduceAsync(List<string> fileEntries)
        {
            foreach (var fileEntry in fileEntries)
            {
                while (await _writer.WaitToWriteAsync().ConfigureAwait(false))
                if (_writer.TryWrite(fileEntry))
                    break;               
            }

            Console.WriteLine("Producer completed");            
        }
    }
}