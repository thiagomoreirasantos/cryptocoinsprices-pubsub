
namespace cryptcoinsprices
{
    public class FileProcessor : IDisposable
    {
        public static Task<string> ProcessAsync(string file)
        {
            try
            {
                Console.WriteLine($"Processing {file}");
                var content = File.ReadAllTextAsync(file);
                return content;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing files.{ex.ToString()}");
            }
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}