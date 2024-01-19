namespace cryptcoinsprices
{
    public class FileProcessor
    {
        public const string DIRECTORY_PATH = "/home/thiago/repo/crypto-coins-prices/src/cryptcoinsprices/archive";
        public static async Task<List<string>> ProcessAsync()
        {
            List<string> fileEntries = new List<string>();
            string[] directories = Directory.GetDirectories(DIRECTORY_PATH);
            
            var fileTasks = directories.Select(directory => Task.Run(() => Directory.GetFiles(directory)));

            var filesArrays = await Task.WhenAll(fileTasks);

            return filesArrays.SelectMany(files => files).ToList();           
            // return fileEntries;       
        }
    }
}