
namespace cryptcoinsprices
{
    public class FileProcessor
    {
        public const string DIRECTORY_PATH = "/home/thiago/repo/crypto-coins-prices/src/cryptcoinsprices/archive";
        public static async Task<List<string>> ProcessAsync()
        {
            List<string> fileCollection = new List<string>();

            string[] directories = Directory.GetDirectories(DIRECTORY_PATH);

            var fileTasks = directories.SelectMany(directory => Directory.GetFiles(directory))
                                    .Select(filePath => ReadFileAsync(filePath));

            var filesArrays = await Task.WhenAll(fileTasks);

            fileCollection.AddRange(filesArrays);
            
            return fileCollection;                  
        }

        private static async Task<string> ReadFileAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }
    }
}