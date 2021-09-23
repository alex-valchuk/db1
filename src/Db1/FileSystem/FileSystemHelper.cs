using System.IO;
using System.Threading.Tasks;
using Db1.FileSystem.Abstractions;

namespace Db1.FileSystem
{
    public class FileSystemHelper : IFileSystemHelper
    {
        public bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }

        public Task<string> ReadAllTextAsync(string fileName)
        {
            return File.ReadAllTextAsync(fileName);
        }

        public Task WriteAllTextAsync(string fileName, string contents)
        {
            return File.WriteAllTextAsync(fileName, contents);
        }
    }
}