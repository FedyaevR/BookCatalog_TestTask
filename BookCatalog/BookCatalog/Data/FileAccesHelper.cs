using Xamarin.Essentials;

namespace BookCatalog.Data
{
    public class FileAccesHelper
    {
        public static string GetLocalFilePath(string fileName)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, fileName);
        }
    }
}
