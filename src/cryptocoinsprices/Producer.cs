using System.Threading.Channels;

namespace cryptocoinsprices
{
    public class Producer
    {
        public async Task<List<string>> ProduceAsync(List<string> fileEntries)
        {
            var channel = Channel.CreateBounded<string>(
                new BoundedChannelOptions(1_000)
                {
                    SingleWriter = true,
                    SingleReader = false,
                    AllowSynchronousContinuations = false,
                    FullMode = BoundedChannelFullMode.DropWrite
                });       
        }
    }
}